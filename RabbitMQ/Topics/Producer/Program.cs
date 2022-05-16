using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 创建数据
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("China.henan.xy", "信阳：20~30℃");
            data.Add("China.henan.zz", "郑州：22~24℃");
            data.Add("China.henan.pds", "平顶山：10~16℃");
            data.Add("China.guangdong.dg", "东莞：23~26℃");
            data.Add("China.guangdong.sz", "深圳：23~25℃");
            data.Add("US.niuyue", "美国纽约：35~50℃");

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            // 建立连接
            using var connection = factory.CreateConnection();
            // 建立通道
            using var channel = connection.CreateModel();

            // 声明topic类型交换机
            channel.ExchangeDeclare(exchange: "testtopic", type:"topic",durable:true);

            foreach (var item in data)
            {
                // 发送消息
                channel.BasicPublish(exchange: "testtopic", routingKey:item.Key,body:Encoding.UTF8.GetBytes(item.Value));
            }

            Console.WriteLine("已发送消息");
            Console.ReadLine();

        }
    }
}
