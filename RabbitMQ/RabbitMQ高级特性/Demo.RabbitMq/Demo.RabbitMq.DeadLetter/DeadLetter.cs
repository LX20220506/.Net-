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
    public class DeadLetter : IHostedService
    {
        private readonly RabbitConnection _connection;
        private readonly IPrcOrderService _orderService;

        public List<QueueInfo> Queues { get; set; } = new List<QueueInfo>();

        public DeadLetter(RabbitConnection connection, IPrcOrderService orderService) {
            _connection = connection;
            _orderService = orderService;

            Queues.Add(new QueueInfo() { 
                Exchange= "dead_letter_exchange",
                ExchangeType="direct",
                Queue="dead_letter_queue",
                RoutingKey="dead_letter_routing_key",
                OnReceived= Received
            });
        }

        public void Received(RabbitMessageEntity rabbitMessage) {
            Console.WriteLine($"DeadLetter Received Message:{rabbitMessage.Contnet}");
            OrderMessage orderMessage = JsonConvert.DeserializeObject<OrderMessage>(rabbitMessage.Contnet);

            orderMessage.OrderInfo.Status = 2;
            Console.WriteLine("超时未处理");

            _orderService.UpdateOrderStatus(orderMessage);

            rabbitMessage.Consumer.Model.BasicAck(rabbitMessage.BasicDeliver.DeliveryTag,true);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RabbitMQ正在启动.....");
            RabbitChannelManager channelManager = new RabbitChannelManager(_connection);
            foreach (var queueInfo in Queues)
            {
                RabbitChannelConfig channelConfig = channelManager.CreateReceivChannel(queueInfo.ExchangeType,queueInfo.Exchange,
                    queueInfo.Queue,queueInfo.RoutingKey);
                channelConfig.OnReceivedCallback = queueInfo.OnReceived;
            }
            Console.WriteLine("RabbitMQ已启动");
            

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
