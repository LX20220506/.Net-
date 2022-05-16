using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer2
{
    /// <summary>
    /// 消费者
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            // 建立连接
            using var connection = factory.CreateConnection();
            // 建立通信
            using var channel = connection.CreateModel();

            // 声明fanout类型交换机
            channel.ExchangeDeclare(exchange: "test", type:"fanout",durable:true,autoDelete:false,arguments:null);

            // 创建一个临时队列，接受虚拟机的消息
            var queueName = channel.QueueDeclare().QueueName;// 拿到临时队列的名称
            // 交换机绑定队列
            channel.QueueBind(queue:queueName,exchange:"test",routingKey:"");
            

            // 触发事件
            var consumer = new EventingBasicConsumer(channel);
            // 添加触发事件的回调方法
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine($"百度天气：{Encoding.UTF8.GetString(ea.Body)}");
            };

            Console.WriteLine("准备接收消息");

            // 接受消息
            channel.BasicConsume(queue: queueName, autoAck:false,consumer:consumer);
            Console.ReadLine();
        }
    }
}
