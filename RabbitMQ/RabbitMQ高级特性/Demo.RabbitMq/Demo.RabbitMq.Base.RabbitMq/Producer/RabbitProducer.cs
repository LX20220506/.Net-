using Demo.RabbitMq.Base.RabbitMq.Config;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Base.RabbitMq.Producer
{
    public class RabbitProducer : IRabbitProducer
    {
        private readonly RabbitConnection _rabbitContext;

        public RabbitProducer(RabbitConnection rabbitContext) {
            _rabbitContext = rabbitContext;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="props"></param>
        /// <param name="content"></param>
        public void Publish(string exchange, string routingKey, IDictionary<string, object> props, string content)
        {
            // 建立通信
            IModel channel = _rabbitContext.GetRabbitMqConnection().CreateModel();

            // channel.ExchangeDeclare(exchange:exchange,type:);// 不建立交换机？？

            // 创建基本属性参数
            var prop = channel.CreateBasicProperties();

            if (props.Count>0)
            {
                var delay = props["x-delay"];
                prop.Expiration = delay.ToString();
            }

            // 发送消息
            channel.BasicPublish(exchange,routingKey,false,prop,Encoding.UTF8.GetBytes(content));
        }
    }
}
