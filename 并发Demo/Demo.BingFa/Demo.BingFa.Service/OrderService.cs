using Demo.BingFa.Base.Redis;
using Demo.BingFa.Entity;
using Demo.BingFa.IRepository;
using System;
using System.Threading.Tasks;
using Demo.BingFa.Entity.RabbitMqMessageModel;

namespace Demo.BingFa.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly RedisHelper _redis;

        public OrderService(IRepositoryWrapper repositoryWrapper,RedisHelper redis) {
            _repository = repositoryWrapper;
            this._redis = redis;
        }

        public async Task CreateOrder(int userId, int goodsId)
        {

            Goods goods =  _repository.Goods.FindAsync(g=>g.Id==goodsId);

            if (goods.Stock==0)
            {
                Console.WriteLine("return");
                return;
            }

            // 库存减1
            goods.Stock = goods.Stock - 1;

            _repository.Goods.Update(goods);

            Order order = new Order() {UserId=userId,GoodsId=goodsId,State=0,CreateTime=DateTime.Now };

            _repository.Order.Create(order);
            await _repository.SaveAsync();
            Console.WriteLine($"用户：{userId}----商品：{goodsId}");
        }

        /// <summary>
        /// 创建秒杀订单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public async Task CreateSeckillOrder(int userId, int goodsId)
        {
            // 创建订单
            Order order = new Order() { UserId = userId, GoodsId = goodsId,State=0, CreateTime = DateTime.Now };
            _repository.Order.Create(order);

            await _repository.SaveAsync();

            // 将订单加入缓存，防止多次下单
            await _redis.StringSetAsync<Order>(userId + "_" + goodsId, order);
        }

        public void Update(Order order) {
            _repository.Order.Update(order);
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="seckillGoodsMesage">秒杀商品信息</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public async Task UpdateOrderState(SeckillGoodsMesage seckillGoodsMesage,int state) {
           
            await _redis.DeleteKey(seckillGoodsMesage.userid + "_" + seckillGoodsMesage.goodsid);
            if (state==1)
            {

                // 数据库：修改订单状态，还原库存
                Order order1 = _repository.Order.FindAsync(o => o.GoodsId == seckillGoodsMesage.goodsid && o.UserId == seckillGoodsMesage.userid);
                order1.State = state;
                await _repository.SaveAsync();
                return;
            }
            
            // redis：删除订单，还原库存
            
            int stock = int.Parse(await _redis.StringGetAsync("SECKILL_GOODS_STOCK"));
            await _redis.StringSetAsync("SECKILL_GOODS_STOCK", stock - 1);

            // 数据库：修改订单状态，还原库存
            Order order2 = _repository.Order.FindAsync(o => o.GoodsId == seckillGoodsMesage.goodsid && o.UserId == seckillGoodsMesage.userid);
            order2.State = state;
            Goods goods = _repository.Goods.FindAsync(o => o.Id == seckillGoodsMesage.goodsid);
            goods.Stock = goods.Stock + 1;

            await _repository.SaveAsync();
        }

        
    }
}
