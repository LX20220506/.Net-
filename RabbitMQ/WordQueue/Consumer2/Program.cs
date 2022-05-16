using RabbitMQ.Client;
using System;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading;

namespace Consumer2
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            // 建立连接
            using var connection = factory.CreateConnection();
            // 打开通信“管道”
            using var channel = connection.CreateModel();

            // 找到queue
            channel.QueueDeclare(queue:"test",durable:true,exclusive:false,autoDelete:false,arguments:null);

            // 声明触发事件
            var consumer = new EventingBasicConsumer(channel);
            // 添加回调函数
            consumer.Received += (model, ea) => {
                // 阻塞(模仿耗时处理)
                Thread.Sleep(300);

                // 接受消息
                byte[] body = ea.Body;
                string message = Encoding.UTF8.GetString(body);

                // 确认一条或多条消息
                channel.BasicAck(deliveryTag:ea.DeliveryTag,multiple:false);

                Console.WriteLine(message);
            };

            // 公平调度，消费能力强的多消费 
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);// 每次获取一条消息，处理完成后再去获取新的消息

            channel.BasicConsume(queue:"test",autoAck:false,consumer:consumer);
            Console.WriteLine("接受message");
            Console.ReadLine();

        }
    }
}
