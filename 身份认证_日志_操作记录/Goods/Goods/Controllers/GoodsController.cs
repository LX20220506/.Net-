using Goods.EF;
using Goods.Entity.Models;
using Goods.Entity.ViewModels;
using Goods.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly GoodsDbContext _goodsDbContext;
        private readonly ILogger<GoodsController> _logger;

        public GoodsController(GoodsDbContext goodsDbContext, ILogger<GoodsController> logger)
        {
            _goodsDbContext = goodsDbContext;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin,business,user")]
        public IActionResult GetGoodsList() {
            var list = _goodsDbContext.GoodsInfo.ToList();
            return Ok(new ApiResult(data: list));
        }

        [HttpPost]
        [Authorize(Roles = "business")]
        public async Task<IActionResult> CreateGoods(AddGoodsRequest addGoodsRequest) {
            GoodsInfo goods = new GoodsInfo(addGoodsRequest.Name,addGoodsRequest.Price,addGoodsRequest.Desc);
            await _goodsDbContext.GoodsInfo.AddAsync(goods);

            if (await _goodsDbContext.SaveChangesAsync()>0)
            {
                return Ok(new ApiResult(msg: "添加成功"));
            }
            return Ok(new ApiResult(msg: "添加失败",stateCode:500));
        }

        [HttpPut]
        [Authorize(Roles = "business")]
        public async Task<IActionResult> EnidGoods(GoodsInfo oldGoods) {
            GoodsInfo goods =  _goodsDbContext.GoodsInfo.SingleOrDefault(g => g.Id == oldGoods.Id);

            if (goods==null)
            {
                return Ok(new ApiResult(msg: "系统错误",stateCode:500));
            }

            goods.GoodsName = oldGoods.GoodsName;
            goods.GoodsPrice = oldGoods.GoodsPrice;
            goods.GoodsDesc = oldGoods.GoodsDesc;

            if (await _goodsDbContext.SaveChangesAsync()>0)
            {
                return Ok(new ApiResult(msg: "修改成功"));
            }
            return Ok(new ApiResult(msg: "系统错误", stateCode: 500));
        }

    }
}
