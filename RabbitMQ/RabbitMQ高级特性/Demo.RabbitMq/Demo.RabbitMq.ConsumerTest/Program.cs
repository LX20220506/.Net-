using System;
using Demo.RabbitMq.Base.RabbitMq.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.RabbitMq.ConsumerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var RabbitConfig = config.GetSection("RabbitMQ");

            var host = new HostBuilder()
                .ConfigureServices(collection => collection
                    .AddSingleton(new RabbitConnection(RabbitConfig.Get<RabbitMqOptions>())) // 注入RabbitMQ的连接器
                    .AddSingleton<IHostedService, Test>()) // IHostedService接口是一个托管服务，当服务启动时，会和服务一起启动
                .Build();

            host.Run();
        }
    }
}
