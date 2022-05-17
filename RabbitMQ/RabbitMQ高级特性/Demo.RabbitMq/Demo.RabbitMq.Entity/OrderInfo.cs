using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Entity
{
    public class OrderInfo
    {
        public int GoodsCount { get; set; }
        public string  GoodsName { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
    }
}
