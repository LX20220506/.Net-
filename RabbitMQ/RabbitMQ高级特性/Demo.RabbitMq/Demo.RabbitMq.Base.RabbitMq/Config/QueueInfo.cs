using Demo.RabbitMq.Base.RabbitMq.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Base.RabbitMq.Config
{
    public class QueueInfo
    {
        public string ExchangeType { get; set; }

        public string Exchange { get; set; }

        public string Queue { get; set; }

        public string  RoutingKey { get; set; }

        public IDictionary<string,object> Props { get; set; }

        public Action<RabbitMessageEntity> OnReceived { get; set; }
    }
}
