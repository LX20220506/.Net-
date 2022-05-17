using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Demo.RabbitMq.Base.RabbitMq.Config;
using Demo.RabbitMq.Base.RabbitMq.Consumer;
using Demo.RabbitMq.Entity;
using Demo.RabbitMq.Service;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Demo.RabbitMq.DeadLetter
{
    /// <summary>
    /// 死信队列
    /// </summary>
    public class DeadLetter : IHostedService
    {
        private readonly RabbitConnection _connection;// rabbitMQ连接器
        private readonly IPrcOrderService _orderService; // 订单完成后的服务

        public List<QueueInfo> Queues { get; set; } = new List<QueueInfo>();// 队列信息

        public DeadLetter(RabbitConnection connection, IPrcOrderService orderService) {
            _connection = connection;
            _orderService = orderService;

            Queues.Add(new QueueInfo() { // 初始化队列信息
                Exchange= "dead_letter_exchange", // 设置死信交换机（要与延迟队列中的x-dead-letter-exchange属性值保持一致）
                RoutingKey = "dead_letter_routing_key", // 设置死信的路由Key（要与延迟队列中的x-dead-letter-routing-key属性值保持一致）
                ExchangeType ="direct",  // 设置交换机类型
                Queue="dead_letter_queue",  // 设置队列名称
                OnReceived= Received // 执行的业务
            });
        }

        // 业务代码
        public void Received(RabbitMessageEntity rabbitMessage) {
            Console.WriteLine($"DeadLetter Received Message:{rabbitMessage.Contnet}");
            OrderMessage orderMessage = JsonConvert.DeserializeObject<OrderMessage>(rabbitMessage.Contnet);

            orderMessage.OrderInfo.Status = 2;
            Console.WriteLine("超时未处理");

            _orderService.UpdateOrderStatus(orderMessage);
            // 设置消息确认
            rabbitMessage.Consumer.Model.BasicAck(rabbitMessage.BasicDeliver.DeliveryTag,true);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RabbitMQ正在启动.....");
            // 建立连接 并创建通道
            RabbitChannelManager channelManager = new RabbitChannelManager(_connection);

            foreach (var queueInfo in Queues)
            {
                // 开始接受消息
                RabbitChannelConfig channelConfig = channelManager.CreateReceivChannel(queueInfo.ExchangeType,queueInfo.Exchange,
                    queueInfo.Queue,queueInfo.RoutingKey);
                channelConfig.OnReceivedCallback = queueInfo.OnReceived;
            }
            Console.WriteLine("RabbitMQ已启动");
            

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
