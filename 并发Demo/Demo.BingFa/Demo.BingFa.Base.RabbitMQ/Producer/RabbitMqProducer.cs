using System.Collections.Generic;
using Demo.BingFa.Base.RabbitMQ.Config;
using RabbitMQ.Client;
using System.Text;

namespace Demo.BingFa.Base.RabbitMQ.Producer
{
    public class RabbitMqProducer: IRabbitMqProducer
    {
        private readonly RabbitMqConnection _connection;

        public RabbitMqProducer(RabbitMqConnection connection)
        {
            this._connection = connection;
        }

        public void Publish(string exchange,string routingKey,IDictionary<string,object> props,string body) {
            IModel channel= _connection.GetConnection().CreateModel();

            // 创建基本属性
            var prop = channel.CreateBasicProperties(); // 设置过期时间
            if (props.Count>0)
            {
                // 拿到key为 x-delay 的值
                var delay = props["x-delay"];
                // 设置到期时间
                prop.Expiration = delay.ToString();
            }

            // 发送消息
            channel.BasicPublish(exchange:exchange, routingKey:routingKey, mandatory: false,basicProperties: prop, body:Encoding.UTF8.GetBytes(body)) ;
        }
    }
}
