using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    /// <summary>
    /// 生产者
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            // 建立连接
            using var connection = factory.CreateConnection();
            // 建立通道
            using var channel = connection.CreateModel();

            string message = "深圳20~26℃";
            byte[] body = Encoding.UTF8.GetBytes(message);
            // 声明交换机
            channel.ExchangeDeclare(exchange:"test",type:"fanout",durable:true,autoDelete:false,arguments:null);

            // 发送消息
            channel.BasicPublish(exchange: "test", routingKey:"",body:body);
            Console.WriteLine("Send Message："+message);

            Console.ReadLine();
        }
    }
}
