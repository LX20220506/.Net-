using Demo.RabbitMq.Base.RabbitMq.Producer;
using Demo.RabbitMq.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Demo.RabbitMq.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRabbitProducer _rabbitProducer;

        public OrderService(IRabbitProducer rabbitProducer) {
            _rabbitProducer = rabbitProducer;
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"send message:{message}");
            _rabbitProducer.Publish("test","",new Dictionary<string,object>(),message);
        }

        public void SendMessage() {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.GoodsCount = 1;
            orderInfo.GoodsName = "袜子";
            orderInfo.Status = 0;
            orderInfo.UserId = 1;
            Account account = new Account();
            account.UserName = "夏";
            account.Password = "123";
            account.Phone = "13311224455";
            account.Email = "456das@139.com";
            OrderMessage message = new OrderMessage();
            message.Account = account;
            message.OrderInfo = orderInfo;

            Console.WriteLine("短信/邮件异步通知");
            Console.WriteLine($"Send Message:{JsonConvert.SerializeObject(message)}");

            // 发送消息
            _rabbitProducer.Publish(exchange: "Order", routingKey: "",
                props: new Dictionary<string, object> { // 添加属性
                            { "x-delay",1000*20} // 增加TTL过期时间，（当过期时间过后，消息任未被处理，则消息会成为死信）
                        },
                content: JsonConvert.SerializeObject(message));
        }
    }
}
