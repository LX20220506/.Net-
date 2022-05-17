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
        private readonly RabbitConnection _connection;
        private readonly IPrcOrderService _orderservice;

        public List<QueueInfo> Queues { get; set; } = new List<QueueInfo>();


        public Order(RabbitConnection connection, IPrcOrderService orderservice) {
            _connection = connection;
            _orderservice = orderservice;

            Queues.Add(new QueueInfo
            {
                Exchange = "Order",
                ExchangeType = "direct",
                Queue = "Order",
                RoutingKey = "",
                Props = new Dictionary<string, object>() {
                    {"x-dead-letter-exchange","dead_letter_exchange" },
                    {"x-dead-letter-routing-key","dead_letter_routing_key" }
                },
                OnReceived = Received
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
            Task.Factory.StartNew(()=> {
                many = Console.ReadLine();
            }).Wait(20*1000);
            if (many=="100")
            {
                orderMessage.OrderInfo.Status = 1;

                Console.WriteLine("支付完成");

                _orderservice.UpdateOrderStatus(orderMessage);

                rabbitMessageEntity.Consumer.Model.BasicAck(rabbitMessageEntity.BasicDeliver.DeliveryTag, true);
            }
            else
            {
                // 重新尝试几次依然失败
                Console.WriteLine("等待一定时间失效的超时未支付订单");
                rabbitMessageEntity.Consumer.Model.BasicNack(rabbitMessageEntity.BasicDeliver.DeliveryTag,false,false);
            }
            

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("正在启动RabbitMQ......");

            RabbitChannelManager channelManager = new RabbitChannelManager(_connection);
            foreach (var queueInfo in Queues)
            {
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
