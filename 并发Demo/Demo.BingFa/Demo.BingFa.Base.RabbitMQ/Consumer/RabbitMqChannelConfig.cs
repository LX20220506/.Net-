using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.BingFa.Base.RabbitMQ.Consumer
{

    public class RabbitMqChannelConfig
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 交换机类型
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// 连接器
        /// </summary>
        public IConnection Connection { get; set; }

        /// <summary>
        /// 消费事件
        /// </summary>
        public EventingBasicConsumer Consumer { get; set; }

        /// <summary>
        /// 用来接受外部的业务代码
        /// </summary>
        public Action<RabbitMqMessageEntity> OnReceivedCallback { get; set; }

        public RabbitMqChannelConfig(string exchange,string exchangeType,string queue,string routingKey) {
            this.ExchangeName = exchange;
            this.ExchangeType = ExchangeType;
            this.QueueName = queue;
            this.RoutingKey = routingKey;
        }


        /// <summary>
        ///  消息队列中接受消息的回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Receive(object sender, BasicDeliverEventArgs args) {
            
            // 消息载体；将接受的消息存储起来
            RabbitMqMessageEntity messageEntity = new RabbitMqMessageEntity();

            try
            {
                // RabbitMQ更新后，这里的args.Body是 ReadOnlyMemory<byte>类型（之前是byte[]）
                // 这个类型无法直接转换成字符串，需要使用ToArray()，将ReadOnlyMemory<byte>类型转换为byte[]类型
                messageEntity.Content = Encoding.UTF8.GetString(args.Body.ToArray());
                messageEntity.BaseExceptionEvent = args;
                messageEntity.Consumer = (EventingBasicConsumer)sender;
            }
            catch (Exception e)
            {
                messageEntity.Error = true;
                messageEntity.Code = 500;
                messageEntity.ErrorMesage = $"订阅出错--{e.Message}";
                messageEntity.Exception = e;
            }

            // 将接受的消息通过委托传递给业务代码
            OnReceivedCallback.Invoke(messageEntity);

            // 确认一条或多条已传递的消息
            //channel.BasicAck(deliveryTag: messageEntity.BaseExceptionEvent.DeliveryTag, multiple: false);// 进行消息确认
        }
    }
}
