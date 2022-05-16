using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            // 建立连接
            using var connection = factory.CreateConnection();
            // 创建通信
            using var channel = connection.CreateModel();

            /*
             * 创建队列
             * queue：队列名称
             * durable：是否持久化
             * exclusive：是否队列私有化；false表示所有消费者都可以使用，true表示只有第一个消费者可以使用
             * autoDelete：是否自动删除
             * arguments：其他额外参数为null
             */
            channel.QueueDeclare(queue:"test",durable:true,exclusive:false,autoDelete:false,arguments:null);

            string message = "hello word";

            // 将发送的消息转换为二进制
            byte[] body = Encoding.UTF8.GetBytes(message);

            /*
             * exchange：交换机，暂时用不到，在发布订阅时才会用到
             * routingKey：路由Key （如果没有队列的话，这个就是队列名称）
             * basicProperties：额外的设置属性
             * body：要传递的二进制消息
             */
            channel.BasicPublish(exchange:"",routingKey:"test",basicProperties:null,body:body);

            Console.WriteLine($"消息：{message}已发送");

            Console.ReadLine();
        }
    }
}
