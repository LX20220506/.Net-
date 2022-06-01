using Demo.BingFa.Base.RabbitMQ.Config;
using Demo.BingFa.Base.RabbitMQ.Consumer;
using Demo.BingFa.Service;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Demo.BingFa.Entity.RabbitMqMessageModel;

namespace DeadLetter
{
    /// <summary>
    /// 死信队列
    /// </summary>
    public class DeadLetterService : IHostedService
    {
        private readonly RabbitMqConnection _connection;
        private readonly IOrderService _orderService;
        private List<QueueInfo> Queues = new List<QueueInfo>();

        public DeadLetterService(RabbitMqConnection connection, IOrderService orderService)
        {
            this._connection = connection;
            this._orderService = orderService;
            Queues.Add(new QueueInfo()
            {
                ExchangeName = "dead_letter_exchange",// 和订单支付消费者中的 死信交换机属性相同
                ExchangeType = "direct",
                QueueName = "DeadLetter",
                RoutingKey = "dead_letter_routing_key",// 和订单支付消费者中的 死信路由属性相同
                OnReceivedCallback = Received
            });
        }

        public void Received(RabbitMqMessageEntity messageEntity) {
            try
            {
                Console.WriteLine($"Receive Message： {messageEntity.Content}");
                SeckillGoodsMesage seckillGoodsMesage = JsonConvert.DeserializeObject<SeckillGoodsMesage>(messageEntity.Content);
                // 修改状态
                _orderService.UpdateOrderState(seckillGoodsMesage,2);

                Console.WriteLine("您未在规定时间内支付金额或支付金额不足，秒杀订单已销毁......");

                // 确认消息
                messageEntity.Consumer.Model.BasicAck(messageEntity.BaseExceptionEvent.DeliveryTag,true);

            }
            catch (Exception e)
            {
                Console.WriteLine($"死信队列报错---{e.Message}");
                throw;
            }
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {

            Console.WriteLine("开始启动RabbitMQ。。。。。。。。");
            RabbitMqChannelMessage channelMessage = new RabbitMqChannelMessage(_connection);
            foreach (var queueInfo in Queues)
            {
                //接受消息
                RabbitMqChannelConfig channelConfig = channelMessage.CreateReceiveChannel(queueInfo.QueueName,queueInfo.ExchangeName,queueInfo.ExchangeType,queueInfo.RoutingKey,queueInfo.Props);
                channelConfig.OnReceivedCallback = queueInfo.OnReceivedCallback;
            }
            Console.WriteLine("已启动RabbitMQ。。。。。。。。。");
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
