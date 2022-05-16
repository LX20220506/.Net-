using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            // 建立连接
            using var connextion = factory.CreateConnection();
            // 建立通信
            using var channel = connextion.CreateModel();

            /*
            * 创建队列
            * queue：队列名称
            * durable：是否持久化
            * exclusive：是否队列私有化；false表示所有消费者都可以使用，true表示只有第一个消费者可以使用
            * autoDelete：是否自动删除
            * arguments：其他额外参数为null
            */
            channel.QueueDeclare(queue:"test",durable:true,exclusive:false,autoDelete:false,arguments:null);

            var consumer = new EventingBasicConsumer(channel);
            // 触发事件，回调方法接受队列中的消息
            consumer.Received += (model, ea) => {
                // 接收消息
                byte[] body = ea.Body;
                string message = Encoding.UTF8.GetString(body);

                channel.BasicAck(ea.DeliveryTag,false);

                Console.WriteLine($"消息：{message}已被接受");
            };

            /*
             * queue：队列的名称
             * autoAck：是否自动确认
             * consumer：继承IBasicConsumer接口的实例对象（触发事件）
             */
            channel.BasicConsume(queue:"test",autoAck:false,consumer:consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
