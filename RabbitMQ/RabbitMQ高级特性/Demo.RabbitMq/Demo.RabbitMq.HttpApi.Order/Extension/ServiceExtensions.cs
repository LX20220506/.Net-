using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.RabbitMq.Base.RabbitMq.Config;

namespace Demo.RabbitMq.HttpApi.Order.Extension
{
    public static class ServiceExtensions
    {

        /// <summary>
        /// 注入 RabbitContext 的对象，使所有地方都可以和RabbitMQ建立连接
        /// </summary>
        public static void AddConfigureRabbitContext(this IServiceCollection services,IConfiguration config) {
            var section = config.GetSection("RabbitMQ");

            // ioc中注入RabbitContext对象
            services.AddSingleton(
                new RabbitConnection(
                    section.Get<RabbitMqOptions>() // 将配置类中的RabbitMQ配置，映射成 RabbitMqOptions类
                    )
                );
        }
    }
}
