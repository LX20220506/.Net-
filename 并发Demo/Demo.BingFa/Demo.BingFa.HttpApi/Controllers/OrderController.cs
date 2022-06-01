using Demo.BingFa.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Demo.BingFa.HttpApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder(int userid, int goodsid) {
            await _orderService.CreateOrder(userid, goodsid);
            return Ok();
        }

        [HttpPost]
        public IActionResult Test(int userid, int goodsid)
        {
            Console.WriteLine($"userid:{userid}---goodsid:{goodsid}");
            return NotFound(userid);
        }

        [HttpPost]
        public IActionResult Test1(int id) {
            return Ok();
        }
 
    }
}
