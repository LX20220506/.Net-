using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMq.Base.RabbitMq.Consumer
{
    /// <summary>
    /// RabbitMQ中的消息总载体
    /// </summary>
    public class RabbitChannelConfig
    {
        public string ExchangeTypeName { get; set; }

        public string ExchangeName { get; set; }

        public string QueueName { get; set; }

        public string RoutingKey { get; set; }

        public IConnection Connection { get; set; }

        // 消费事件
        public EventingBasicConsumer Consumer { get; set; }

        /// <summary>
        /// 外部订阅消费者通知委托（需要执行的业务）
        /// </summary>
        public Action<RabbitMessageEntity> OnReceivedCallback { get; set; }

        public RabbitChannelConfig(string exchangeType,string exchange,string queue,string routingKey) {
            this.ExchangeTypeName = exchangeType;
            this.ExchangeName = exchange;
            this.QueueName = queue;
            this.RoutingKey = routingKey;
        }

        /// <summary>
        /// （这里只是将接受的数据重新封装成RabbitMessageEntity类型，然后当做OnReceivedCallback委托中方法的参数，传递给真正的业务代码）
        /// 回调事件的方法，接受到消息后，执行的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Receive(object sender,BasicDeliverEventArgs args) {

            RabbitMessageEntity body = new RabbitMessageEntity();

            try
            {
                string message = Encoding.UTF8.GetString(args.Body);
                body.Contnet = message;
                
                body.Consumer = (EventingBasicConsumer)sender;
                body.BasicDeliver = args;

            }
            catch (Exception e)
            {

                body.ErrorMessage = $"订阅-出错{e.Message}";
                body.Error = true;
                body.Exception = e;
                body.Code = 500;
            }

            // 执行委托，委托中是业务代码，将封装好的数据，传递给业务代码
            OnReceivedCallback?.Invoke(body);

        }

    }
}
