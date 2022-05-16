using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Base.RabbitMq.Producer
{
    public interface IRabbitProducer
    {
        void Publish(string exchange,string routingKey,IDictionary<string,object> props,string content);
    }
}
