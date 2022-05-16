using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer1
{
    /// <summary>
    /// 河南天气
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            // 创建连接
            using var connection = factory.CreateConnection();
            // 建立通信
            using var channel = connection.CreateModel();

            // 声明direct类型的交换机
            channel.ExchangeDeclare(exchange: "testdirect", type:"direct",durable:true);
            // 创建临时队列 并绑定（QueueDeclare()会创建一个临时队列）
            string queueName = channel.QueueDeclare().QueueName;
            // test交换机绑定临时队列，通过设置routingKey参数，来匹配队列中的消息,但是每个路由只能匹配一个
            channel.QueueBind(queue: queueName, exchange: "testdirect", "China.henan.xy");
            channel.QueueBind(queue: queueName, exchange: "testdirect", "China.henan.zz");
            channel.QueueBind(queue: queueName, exchange: "testdirect", "China.henan.pds");

            // 创建消费者触发事件
            var consumer = new EventingBasicConsumer(channel);
            // 添加回调方法
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(ea.Body));
            };

            // 发送消息
            channel.BasicConsume(queue:queueName,autoAck:false,consumer:consumer);

            Console.WriteLine("河南天气：");
            Console.ReadLine();
        }
    }
}
