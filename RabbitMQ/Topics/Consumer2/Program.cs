using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
            // 建立通道
            using var channel = connection.CreateModel();

            // 声明topics类型交换机
            channel.ExchangeDeclare(exchange:"testtopic",type:"topic",durable:true);
            // 将临时队列和交换机绑定
            string queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue:queueName,exchange: "testtopic", routingKey:"US.#");// routingKey通过通配符识别路由，*表示匹配一个单词，#表示匹配所有单词

            // 触发事件（消费者）
            var consumer = new EventingBasicConsumer(channel);
            // 添加回调函数
            consumer.Received += (model, ea) => {
                Console.WriteLine(Encoding.UTF8.GetString(ea.Body));
            };

            // 接受消息
            channel.BasicConsume(queue:queueName,autoAck:false,consumer:consumer);

            Console.WriteLine("美国天气：");
            Console.ReadLine();
        }
    }
}
