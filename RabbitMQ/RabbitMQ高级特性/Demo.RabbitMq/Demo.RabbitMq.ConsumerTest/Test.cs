using Demo.RabbitMq.Base.RabbitMq.Config;
using Demo.RabbitMq.Base.RabbitMq.Consumer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.RabbitMq.ConsumerTest
{
    public class Test : IHostedService
    {
        private readonly RabbitConnection _connection; // 连接器

        // 队列信息集合
        public List<QueueInfo> Queue { get; set; } = new List<QueueInfo>();

        // 依赖注入RabbitConnection 连接器
        public Test(RabbitConnection connection) {
            _connection = connection;
            // 初始化消息队列的配置信息
            Queue.Add(new QueueInfo() { 
                Exchange="test", // 交换机
                ExchangeType= "direct", // 交换机类型
                Queue="test", // 队列名称
                RoutingKey="", // 路由建
                OnReceived= Receive // 将接受消息的触发方法，赋值给委托
            });
        }

        /// <summary>
        /// 接受消息的方法
        /// </summary>
        /// <param name="message"></param>
        public void Receive(RabbitMessageEntity message) {

            Console.WriteLine($"test receive message:{message.Contnet}");
            // 消息确认    消费者处理完成后，将处理成功的消息，返回给队列。
            message.Consumer.Model.BasicAck(message.BasicDeliver.DeliveryTag,true);
        }

        /// <summary>
        /// 当服务启动时，会执行这个方法
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RabbitMQ测试消息接受服务器正在启动");
            // 创建管道处理者
            RabbitChannelManager channelManager = new RabbitChannelManager(_connection);
            foreach (var queueInfo in Queue)
            {
                RabbitChannelConfig channel = channelManager.CreateReceivChannel(queueInfo.ExchangeType,
                    queueInfo.Exchange, queueInfo.Queue, queueInfo.RoutingKey);
                channel.OnReceivedCallback = queueInfo.OnReceived;
            }

            Console.WriteLine("RabbitMQ测试消息接受服务器已启动");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
