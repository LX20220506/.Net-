using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Demo.RabbitMq.Base.RabbitMq.Config;
using Demo.RabbitMq.Service;

namespace Demo.RabbitMq.DeadLetter
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
                .ConfigureServices(collection =>
                {
                    collection.AddSingleton(new RabbitConnection(RabbitConfig.Get<RabbitMqOptions>()));
                    collection.AddScoped<IHostedService, DeadLetter>();
                    collection.AddSingleton<IPrcOrderService, PrcOrderService>();
                })
                .Build();

            host.Run();
        }
    }
}
