using Demo.RabbitMq.Base.RabbitMq.Config;
using Demo.RabbitMq.Base.RabbitMq.Consumer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Demo.RabbitMq.Entity;
using Demo.RabbitMq.Service;

namespace Demo.RabbitMq.Order
{
    public class Order : IHostedService
    {
        private readonly RabbitConnection _connection; // rabbit连接器
        private readonly IPrcOrderService _orderservice; // 订单完成后的服务

        public List<QueueInfo> Queues { get; set; } = new List<QueueInfo>(); // 队列信息


        public Order(RabbitConnection connection, IPrcOrderService orderservice) {
            _connection = connection; // 连接器
            _orderservice = orderservice;

            Queues.Add(new QueueInfo // 设置队列信息
            { 
                Exchange = "Order", // 交换机名称
                ExchangeType = "direct", // 交换机类型
                Queue = "Order",    // 队列名称
                RoutingKey = "",    // 路由Key
                Props = new Dictionary<string, object>() {  // 设置属性
                    {"x-dead-letter-exchange","dead_letter_exchange" }, // 设置死信队列的交换机名称
                    {"x-dead-letter-routing-key","dead_letter_routing_key" } // 设置死信队列的路由Key
                },
                OnReceived = Received // 处理业务的方法
            }) ;
        }

        public void Received(RabbitMessageEntity rabbitMessageEntity) {
            Console.WriteLine($"Order Receive Message:{rabbitMessageEntity.Contnet}");
            OrderMessage orderMessage =JsonConvert.DeserializeObject<OrderMessage>(rabbitMessageEntity.Contnet);

            // 超时支付
            string many = "";
            // 支付处理
            Console.WriteLine("请输入：");
            // 超时支付进行处理
            Task.Factory.StartNew(()=> { // 创建一个新的线程
                many = Console.ReadLine(); 
            }).Wait(20*1000); // wait 等待20秒
            if (many=="100")
            {
                orderMessage.OrderInfo.Status = 1; // 修改状态，改为支付成功

                Console.WriteLine("支付完成");
                // 支付完成后的处理
                _orderservice.UpdateOrderStatus(orderMessage);
                // 确认消息
                rabbitMessageEntity.Consumer.Model.BasicAck(rabbitMessageEntity.BasicDeliver.DeliveryTag, true);
            }
            else
            {
                // 重新尝试几次依然失败后......
                Console.WriteLine("等待一定时间失效的超时未支付订单");
                // 超时后，进入死信队列
                rabbitMessageEntity.Consumer.Model.BasicNack(rabbitMessageEntity.BasicDeliver.DeliveryTag,false,false);
            }
            

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("正在启动RabbitMQ......");
            // 开启连接 并创建接受通道
            RabbitChannelManager channelManager = new RabbitChannelManager(_connection);

            foreach (var queueInfo in Queues)
            {
                // 开始接受消息
                RabbitChannelConfig channelConfig = channelManager.CreateReceivChannel(queueInfo.ExchangeType, queueInfo.Exchange,
                    queueInfo.Queue, queueInfo.RoutingKey, queueInfo.Props);

                channelConfig.OnReceivedCallback = queueInfo.OnReceived;
            }

            Console.WriteLine("RabbitMQ已启动成功");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
