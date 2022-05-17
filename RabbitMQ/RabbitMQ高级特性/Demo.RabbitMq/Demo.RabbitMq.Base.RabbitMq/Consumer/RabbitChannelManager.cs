using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.RabbitMq.Base.RabbitMq.Config;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.RabbitMq.Base.RabbitMq.Consumer
{
    public class RabbitChannelManager
    {
        // 连接器
        public RabbitConnection Connection { get; set; }

        // 注入连接器
        public RabbitChannelManager(RabbitConnection connection) {
            this.Connection = connection;
        }
        /// <summary>
        /// 创建接收管道
        /// </summary>
        public RabbitChannelConfig CreateReceivChannel(string exchangeType,string exchange,string queue,string routingKey,IDictionary<string,object> arguments=null) 
        {
            // 建立通信管道
            IModel model = CreateModel(exchangeType, exchange, queue, routingKey, arguments);
            
            // 创建消费事件
            EventingBasicConsumer consumer = CreateConsumer(model,queue);

            RabbitChannelConfig channel = new RabbitChannelConfig(exchangeType,exchange,queue,routingKey);
            consumer.Received += channel.Receive;
           
            return channel;
        }

        /// <summary>
        /// 建立并配置通信管道
        /// </summary>
        private IModel CreateModel(string exchangeType, string exchange, string queue, string routingKey, IDictionary<string, object> argumernts = null) {
            
            // 设置交换机的类型
            exchangeType = string.IsNullOrEmpty(exchangeType) ? "direct" : exchangeType;
            
            // 先和RabbitMQ建立连接， 之后再创建连接通道
            IModel model = Connection.GetRabbitMqConnection().CreateModel();
            // 设置公平调度
            model.BasicQos(0, 1, false);
            // 定义队列
            model.QueueDeclare(queue,true,false,false,argumernts);
            // 定义交换机
            model.ExchangeDeclare(exchange, exchangeType);
            // 将队列和交换机绑定
            model.QueueBind(queue,exchange,routingKey,argumernts);
           
            // 返回通信通道
            return model;
        }

        /// <summary>
        /// 创建消费者（创建处理消息的事件）并进行消费
        /// </summary>
        /// <param name="model">通信管道</param>
        /// <param name="queue">队列名称</param>
        /// <returns></returns>
        public EventingBasicConsumer CreateConsumer(IModel model, string queue) {
            
            // 创建触发事件
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            
            // 接收消息（消费）
            model.BasicConsume(queue,false,consumer);
            // 返回事件
            return consumer;
        }
        
    }
}
