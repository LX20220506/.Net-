using Demo.BingFa.Base.RabbitMQ.Config;
using Demo.BingFa.Base.RabbitMQ.Consumer;
using Demo.BingFa.Entity.RabbitMqMessageModel;
using Demo.BingFa.Service;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PayConsumer
{
    /// <summary>
    /// 进行支付的消费者
    /// </summary>
    public class PaymentService : IHostedService
    {
        private readonly RabbitMqConnection _connection;
        private readonly IOrderService _orderService;
        private List<QueueInfo> Queues = new List<QueueInfo>();
        public PaymentService(RabbitMqConnection connection,IOrderService orderService) {
            this._connection = connection;
            this._orderService = orderService;
            Queues.Add(new QueueInfo() { 
                ExchangeName= "Payment",
                ExchangeType= "direct",
                QueueName= "Payment",
                RoutingKey="" ,
                OnReceivedCallback= Received,
                Props = new Dictionary<string, object>() {  // 设置属性
                    {"x-dead-letter-exchange","dead_letter_exchange" }, // 设置死信队列的交换机名称
                    {"x-dead-letter-routing-key","dead_letter_routing_key" } // 设置死信队列的路由Key
                },
            });
        }

        public void Received(RabbitMqMessageEntity messageEntity) {
            try
            {
                Console.WriteLine($"Order Receive Message:{messageEntity.Content}");
                SeckillGoodsMesage message = JsonConvert.DeserializeObject<SeckillGoodsMesage>(messageEntity.Content);
                string money = "";
                Console.WriteLine("shu ru jin e:");
                Task.Factory.StartNew(()=> {
                    money = Console.ReadLine();
                }).Wait(20*1000);

                if (money=="3299")
                {
                    Console.WriteLine("支付成功");
                    // 修改订单状态
                    _orderService.UpdateOrderState(message,1);
                    // 确认消息
                    messageEntity.Consumer.Model.BasicAck(messageEntity.BaseExceptionEvent.DeliveryTag,true);
                }
                else
                {
                    Console.WriteLine("未在规定时间内支付订单，取消订单。。。。。。");
                    // 放入死信队列
                    messageEntity.Consumer.Model.BasicNack(messageEntity.BaseExceptionEvent.DeliveryTag,false,false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"支付报错---{e.Message}");
                throw;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("开始启动RabbitMQ。。。。。。");

            RabbitMqChannelMessage channelMessage = new RabbitMqChannelMessage(_connection);

            foreach (var queueInfo in Queues)
            {
                // 接收消息
                RabbitMqChannelConfig channelConfig = channelMessage.CreateReceiveChannel(queueInfo.QueueName,queueInfo.ExchangeName,queueInfo.ExchangeType,queueInfo.RoutingKey,queueInfo.Props);
                channelConfig.OnReceivedCallback = queueInfo.OnReceivedCallback;
            }

            Console.WriteLine("已启动RabbitMQ。。。。。。。。");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
