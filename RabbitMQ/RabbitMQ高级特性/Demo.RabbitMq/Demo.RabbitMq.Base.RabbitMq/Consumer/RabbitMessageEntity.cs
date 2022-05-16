using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.RabbitMq.Base.RabbitMq.Consumer
{
    /// <summary>
    ///  响应信息的载体
    /// </summary>
    public class RabbitMessageEntity
    {
        public EventingBasicConsumer Consumer { get; set; }

        public BasicDeliverEventArgs BasicDeliver { get; set; }

        public int Code { get; set; }

        public string Contnet { get; set; }

        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exception { get; set; }
    }
}
