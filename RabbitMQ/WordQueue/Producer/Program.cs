using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();

            factory.HostName = "localhost";

            // 打开连接
            using var connection = factory.CreateConnection();
            // 建立通信 
            using var channel = connection.CreateModel();

            // 找到队列
            channel.QueueDeclare(queue: "test", durable: true, exclusive: false, autoDelete: false, arguments: null);

            for (int i = 0; i < 1000; i++)
            {
                string message = $"向用{i}发送短信";
                byte[] body = Encoding.UTF8.GetBytes(message);
                // 发送消息
                channel.BasicPublish(exchange: "", routingKey: "test", basicProperties: null, body: body);

                Console.WriteLine(message);
            }

            Console.WriteLine("消息发送完成");
            Console.ReadLine();
        }
    }
}
