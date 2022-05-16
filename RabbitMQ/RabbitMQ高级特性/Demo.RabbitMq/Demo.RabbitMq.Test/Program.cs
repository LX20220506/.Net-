using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.RabbitMq.Base.RabbitMq.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.RabbitMq.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var RabbitConfig = config.GetSection("RabbitMQ");

            var host = new HostBuilder()
                .ConfigureServices(collection => collection
                    .AddSingleton(new RabbitConnection(RabbitConfig.Get<RabbitMqOptions>()))
                    .AddSingleton<IHostedService,ConsumerTest>())
                .Build();

            host.Run();
        }
    }
}
