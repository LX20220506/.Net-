using Demo.BingFa.Base.RabbitMQ.Config;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.BingFa.Base.RabbitMQ.Consumer
{
    public class RabbitMqChannelMessage
    {
        private readonly RabbitMqConnection _connection;

        public RabbitMqChannelMessage(RabbitMqConnection connection)
        {
            this._connection = connection;
        }


        /// <summary>
        /// 创建通道，开始接受消息，创建消息的回调事件
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="exchange"></param>
        /// <param name="exchangeType"></param>
        /// <param name="routingKey"></param>
        /// <param name="props"></param>
        /// <returns></returns>
        public RabbitMqChannelConfig CreateReceiveChannel(string queue, string exchange, string exchangeType, string routingKey, IDictionary<string, object> props = null) {
            // 创建接受通道的所有信息（包括回调函数）
            RabbitMqChannelConfig channelConfig = new RabbitMqChannelConfig(exchange,exchangeType,queue,routingKey);
            
            // 创建通道
            IModel channel = CrateModel(queue, exchange, exchangeType, routingKey, props);

            // 创建回调事件
            EventingBasicConsumer consumer = CreateConsumer(channel,queue);
            // 将设置好的回调事件的方法，将方法添加（多播委托）给回调事件
            consumer.Received += channelConfig.Receive;

            return channelConfig;
        }

        /// <summary>
        /// 创建通信的管道，创建 队列/交换机/路由，并绑定队列；设置公平调度
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="exchange"></param>
        /// <param name="exchangeType"></param>
        /// <param name="routingKey"></param>
        /// <param name="props"></param>
        /// <returns></returns>
        public IModel CrateModel(string queue,string exchange,string exchangeType,string routingKey, IDictionary<string, object> props=null)
        {

            exchangeType = string.IsNullOrEmpty(exchangeType) ? "direct" : exchangeType;

            IModel channel = _connection.GetConnection().CreateModel();

            // 公平调度  每次取一条数据
            channel.BasicQos(0, 1, false);

            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: props);
            channel.ExchangeDeclare(exchange: exchange, type: exchangeType, durable:true,autoDelete:false,arguments:props);
            channel.QueueBind(queue,exchange,routingKey,props);

            return channel;

        }

        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        public EventingBasicConsumer CreateConsumer(IModel channel,string queue) {
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            // 接受消息，autoAck = false：手动确认消息
            channel.BasicConsume(queue,false,consumer);

            return consumer;
        }
    }
}
