using Demo.RabbitMq.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.RabbitMq.HttpApi.Order.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Test(string message) {
            _orderService.SendMessage(message);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetOrderMessage() {
            _orderService.SendMessage();
            return Ok();
        }
    }
}
