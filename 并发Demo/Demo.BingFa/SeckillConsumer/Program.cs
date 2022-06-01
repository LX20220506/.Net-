using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.BingFa.Base.RabbitMQ.Config;
using Microsoft.Extensions.Hosting;
using Demo.BingFa.Service;
using Demo.BingFa.Base.Redis;
using Microsoft.Extensions.Logging;
using Demo.BingFa.Base.RabbitMQ.Producer;
using Demo.BingFa.IRepository;
using Demo.BingFa.Repostory;
using Demo.BingFa.EF;
using Microsoft.EntityFrameworkCore;

namespace SeckillConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var reabbitMqConfig = config.GetSection("RabbitMQ");
            var redisOptions = config.GetSection("Redis");
            var host = new HostBuilder()
                .ConfigureServices(service =>
                {
                    service.AddSingleton(new RabbitMqConnection(reabbitMqConfig.Get<RabbitMqOptions>()));
                    service.AddSingleton<IHostedService, SendEmail>();
                    service.AddScoped<ISeckillService, SeckillService>();
                    service.AddSingleton(new RedisHelper(redisOptions.Get<RedisOptions>()));
                    service.AddLogging(logBuilder =>
                    {
                        logBuilder.AddConsole();
                    });
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
