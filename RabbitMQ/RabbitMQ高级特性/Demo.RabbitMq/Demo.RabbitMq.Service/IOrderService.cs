using Demo.RabbitMq.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Service
{
    public interface IOrderService
    {
        public void SendMessage(string message);
        public void SendMessage();

        
    }
}
