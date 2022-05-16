using Demo.RabbitMq.Base.RabbitMq.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRabbitProducer _rabbitProducer;

        public OrderService(IRabbitProducer rabbitProducer) {
            _rabbitProducer = rabbitProducer;
        }

        public void SendTestMessage(string message)
        {
            Console.WriteLine($"send message:{message}");
            _rabbitProducer.Publish("test","",new Dictionary<string,object>(),message);
        }
    }
}
