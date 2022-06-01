using Demo.BingFa.Base.RabbitMQ.Consumer;
using Demo.BingFa.Base.RabbitMQ.Producer;
using Demo.BingFa.Base.Redis;
using Demo.BingFa.Entity;
using Demo.BingFa.Entity.RabbitMqMessageModel;
using Demo.BingFa.IRepository;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.Service
{
    public class SeckillService : ISeckillService
    {
        private readonly RedisHelper _redis;
        private readonly ILogger<SeckillService> _logger;
        private readonly IRabbitMqProducer _producer;
        private readonly IGoodsService _goodsService;
        private readonly IOrderService _orderService;
        private readonly IRepositoryWrapper _repository;

        public SeckillService(RedisHelper redis,ILogger<SeckillService> logger,IRabbitMqProducer producer,IGoodsService goodsService,IOrderService orderService,IRepositoryWrapper repository) {
            this._redis = redis;
            this._logger = logger;
            this._producer = producer;
            this._goodsService = goodsService;
            this._orderService = orderService;
            this._repository = repository;
        }

        /// <summary>
        /// 秒杀的后续操作（减少数据库库存/添加Order到缓存）
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Seckill(SeckillGoodsMesage message)
        {
            bool checkStock = _goodsService.CheckStock(message.goodsid);
            if (checkStock)
            {
                await _orderService.CreateSeckillOrder(message.userid,message.goodsid);
            }
            else
            {
                Console.WriteLine($"商品id为{message.goodsid} 的商品为库存为空");
            }
        }


        /// <summary>
        /// 发布消息到订阅者
        /// </summary>
        /// <param name="mesage"></param>
        public void SeckillGoodsMessage(SeckillGoodsMesage mesage)
        {
            string body = JsonConvert.SerializeObject(mesage);
            _logger.LogInformation($"Send Message:{body}");

            // 发送给创建秒杀订单服务
            _producer.Publish("seckill_goods", "", new Dictionary<string, object>(), body);

            // 发送给支付订单服务
            _producer.Publish("Payment", "", new Dictionary<string, object>() {
               { "x-delay",20*1000}
            }, body);
        }

        /// <summary>
        /// 支付失败时，删除订单，还原库存
        /// </summary>
        /// <param name="seckillGoodsMesage"></param>
        /// <returns></returns>
        public async Task UpdateSeckill(SeckillGoodsMesage seckillGoodsMesage) {
            
            // redis：删除订单，还原库存
            await _redis.DeleteKey(seckillGoodsMesage.userid+"_"+seckillGoodsMesage.goodsid);
            int stock=int.Parse(await _redis.StringGetAsync("SECKILL_GOODS_STOCK"));
            await _redis.StringSetAsync("SECKILL_GOODS_STOCK", stock - 1);
            

            // 数据库：删除订单，还原库存
            Order order = _repository.Order.FindAsync(o => o.UserId == seckillGoodsMesage.userid && o.GoodsId == seckillGoodsMesage.goodsid);
            Goods goods = _repository.Goods.FindAsync(o => o.Id == seckillGoodsMesage.goodsid);
            goods.Stock = goods.Stock + 1;
            _repository.Order.Remove(order);
            _repository.Goods.Update(goods);


            await _repository.SaveAsync();
        }
    }
}
