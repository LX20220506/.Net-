using Demo.RabbitMq.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Service
{
    public class PrcOrderService : IPrcOrderService
    {
        public void UpdateOrderStatus(OrderMessage orderMessage)
        {
            Console.WriteLine("订单状态修改为" + orderMessage.OrderInfo.Status);
        }
    }
}
