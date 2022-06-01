using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Demo.BingFa.Base.RabbitMQ.Config;
using Demo.BingFa.Repostory;
using Demo.BingFa.IRepository;
using Demo.BingFa.Service;
using Demo.BingFa.Base.RabbitMQ.Producer;
using Demo.BingFa.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Demo.BingFa.Base.Redis;

namespace PayConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();


            var rabbitMqOptions = config.GetSection("RabbitMQ");
            var redisOptions = config.GetSection("Redis");
            var host = new HostBuilder()
                .ConfigureServices(service=> {
                    service.AddSingleton(new RabbitMqConnection(rabbitMqOptions.Get<RabbitMqOptions>()));
                    service.AddSingleton<IHostedService,PaymentService>();

                    service.AddScoped<ISeckillService, SeckillService>();
                    service.AddSingleton(new RedisHelper(redisOptions.Get<RedisOptions>()));
                    //service.AddLogging(logBuilder =>
                    //{
                    //    logBuilder.AddConsole();
                    //});
                    service.AddScoped<IRabbitMqProducer, RabbitMqProducer>();
                    service.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

                    service.AddScoped<IOrderService, OrderService>();
                    service.AddScoped<IUserService, UserService>();
                    service.AddScoped<IGoodsService, GoodsService>();
                    service.AddScoped<ISeckillService, SeckillService>();

                    service.AddDbContext<BingFaDbContext>(options =>
                    {
                        options.UseSqlServer(config.GetConnectionString("MSSQL"));
                    });
                })
                .Build();

            host.Run();
        }
    }
}
