using Demo.BingFa.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.BingFa.HttpApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsService _goodsService;

        public GoodsController(IGoodsService goodsService)
        {
            this._goodsService = goodsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoods(string goodsName, double price, int stock) {
            await _goodsService.CreateGoodsAsync(goodsName,price,stock);
            return Ok();
        }
    }
}
