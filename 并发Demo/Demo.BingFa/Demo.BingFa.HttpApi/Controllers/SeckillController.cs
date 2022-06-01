using Demo.BingFa.Base.RabbitMQ.Producer;
using Demo.BingFa.Base.Redis;
using Demo.BingFa.Entity.RabbitMqMessageModel;
using Demo.BingFa.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.BingFa.HttpApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeckillController : ControllerBase
    {
        private readonly RedisHelper _redis;
        private readonly ILogger<SeckillController> _logger;
        private readonly IGoodsService _goodsService;
        private readonly ISeckillService _seckillService;

        // 用于存储物品状态；
        // 例如：当redis中的商品（goods）没有库存时，就将商品的状态设为false/true，用于后面的判断
        private IDictionary<string, bool> _locaOverMap = new Dictionary<string, bool>();

        public SeckillController(RedisHelper redis, ILogger<SeckillController> logger, IGoodsService goodsService, ISeckillService seckillService)
        {
            _redis = redis;
            _logger = logger;
            this._goodsService = goodsService;
            _seckillService = seckillService;
        }


        [HttpGet]
        public async Task<IActionResult> InitStock() {

            await _redis.StringSetAsync("SECKILL_GOODS_STOCK", 1);
            await _goodsService.UpdateStockAsync(1,1);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> SeckillGoods(int userid, int goodsid)
        {
            // 判断是否有库存
            if (_locaOverMap.Count>0)
            {
                bool over = _locaOverMap["goods"];
                if (over)
                {
                    _logger.LogInformation($"抱歉！没有Stock");
                    return Ok();
                }
            }
             
            // 判断是否重复购买
            if (await _redis.IsKey(userid + "_" + goodsid))
            {
                _logger.LogInformation($"用户{userid}您已抢购，请勿重复购买");
                return Ok();
            }

            // 判断redis中是否还有库存
            int stock = await _redis.StringGetAsync<int>("SECKILL_GOODS_STOCK");
            Console.WriteLine(stock);
            if (stock == 0)
            {
                _locaOverMap["goods"] = true;
                _logger.LogInformation("您未抢到该商品");
                return Ok();
            }
            stock--;
            _redis.StringSetAsync("SECKILL_GOODS_STOCK", stock.ToString());

            // 入队
            SeckillGoodsMesage mesage = new SeckillGoodsMesage();
            mesage.userid = userid;
            mesage.goodsid = goodsid;
            _seckillService.SeckillGoodsMessage(mesage);

            return Ok();
        }
    }
}
