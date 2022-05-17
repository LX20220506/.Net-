using Demo.RabbitMq.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Service
{
    public interface IPrcOrderService
    {
        public void UpdateOrderStatus(OrderMessage orderMessage);
    }
}
