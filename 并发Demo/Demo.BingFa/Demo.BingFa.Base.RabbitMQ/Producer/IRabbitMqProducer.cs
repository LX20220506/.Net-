using System.Collections.Generic;

namespace Demo.BingFa.Base.RabbitMQ.Producer
{
    public interface IRabbitMqProducer
    {
        void Publish(string exchange, string routingKey, IDictionary<string, object> prop, string body);
    }
}
