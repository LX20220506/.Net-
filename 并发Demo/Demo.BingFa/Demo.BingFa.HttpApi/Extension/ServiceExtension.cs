using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Demo.BingFa.EF;
using Microsoft.Extensions.Configuration;
using Demo.BingFa.Base.Redis;
using Demo.BingFa.IRepository;
using Demo.BingFa.Repostory;
using Demo.BingFa.Service;
using Demo.BingFa.Base.RabbitMQ.Producer;
using Demo.BingFa.Base.RabbitMQ.Config;

namespace Demo.BingFa.HttpApi.Extension
{
    public static class ServiceExtension
    {
        /// <summary>
        /// 添加数据库服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDbContextConfiguration(this IServiceCollection services,IConfiguration configuration) {
            services.AddDbContext<BingFaDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSQL"));

            });
        }

        /// <summary>
        ///  添加Redis
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddRedisConfiguration(this IServiceCollection services,IConfiguration configuration) {
            var redisOptions = configuration.GetSection("Redis");
            services.AddSingleton(new RedisHelper(redisOptions.Get<RedisOptions>()));
        }

        /// <summary>
        /// 添加RabbitMQ服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddRabbitMQConfiguration(this IServiceCollection services, IConfiguration configuration) {
            var reabbitMqConfig = configuration.GetSection("RabbitMQ");
            services.AddSingleton(new RabbitMqConnection(reabbitMqConfig.Get<RabbitMqOptions>()));
            services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();
        }

        /// <summary>
        /// 添加仓储
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepository(this IServiceCollection services) {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        /// <summary>
        /// 添加实体服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddEntityService(this IServiceCollection services) {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGoodsService, GoodsService>();
            services.AddScoped<ISeckillService, SeckillService>();
        }
    }
}
