using Demo.BingFa.Base.RabbitMQ.Config;
using Demo.BingFa.Base.RabbitMQ.Consumer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Demo.BingFa.Entity.RabbitMqMessageModel;
using Demo.BingFa.Service;

namespace SeckillConsumer
{
    /// <summary>
    /// 消费订单的消费者
    /// </summary>
    public class SendEmail : IHostedService
    {
        private readonly RabbitMqConnection _connection;
        private readonly ISeckillService _seckillService;
        private List<QueueInfo> Queues = new List<QueueInfo>();

        public SendEmail(RabbitMqConnection connection,ISeckillService seckillService)
        {
            this._connection = connection;
            this._seckillService = seckillService;
            Queues.Add(
                new QueueInfo()
                {
                    ExchangeName = "seckill_goods",
                    ExchangeType = "direct",
                    QueueName = "seckill_goods",
                    RoutingKey = "",
                    OnReceivedCallback = Receive
                }); 
        }

        public async void Receive(RabbitMqMessageEntity messageEntity) {
            Console.WriteLine($"Receive Message： {messageEntity.Content}");
            try
            {
                await Task.Run(async ()=> {
                    SeckillGoodsMesage seckillGoods = JsonConvert.DeserializeObject<SeckillGoodsMesage>(messageEntity.Content);
                    await _seckillService.Seckill(seckillGoods);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            messageEntity.Consumer.Model.BasicAck(messageEntity.BaseExceptionEvent.DeliveryTag,true);
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Ready to start RabbitMQ-----");
            RabbitMqChannelMessage channelMessage = new RabbitMqChannelMessage(_connection);

            foreach (var queueInfo in Queues)
            {
                 RabbitMqChannelConfig channel =channelMessage.CreateReceiveChannel(queueInfo.QueueName,queueInfo.ExchangeName,queueInfo.ExchangeType,queueInfo.RoutingKey);
                channel.OnReceivedCallback = queueInfo.OnReceivedCallback;
            }
            Console.WriteLine("Started RabbitMQ-----");

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
