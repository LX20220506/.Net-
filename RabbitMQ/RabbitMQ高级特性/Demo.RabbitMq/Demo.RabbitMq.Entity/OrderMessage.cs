using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Entity
{
    public class OrderMessage
    {
        public Account Account { get; set; }
        public OrderInfo OrderInfo { get; set; }
    }
}
