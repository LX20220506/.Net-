using Demo.Redis.Base.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo.Redis.EF;
using Demo.Redis.IRepository;
using Demo.Redis.Repository;

namespace Demo.Redis.HttpApi.Extension
{
    public static class ServiceExtension
    {
        /// <summary>
        /// 添加Cors服务
        /// </summary>
        /// <param name="service"></param>
        public static void AddCorsConfiguration(this IServiceCollection service) {
            service.AddCors(option => {
                option.AddPolicy("MyCors", policy => {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        /// <summary>
        /// 添加Redis服务
        /// </summary>
        public static void AddRedisConfiguration(this IServiceCollection services,IConfiguration configuration) {
            var RedisConfig = configuration.GetSection("Redis");
            services.AddSingleton(new RedisHelper(RedisConfig.Get<RedisOption>()));
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<RedisTestDbContext>(options=> {
                options.UseSqlServer(configuration.GetConnectionString("RedisTest"));
            });
        }

        /// <summary>
        /// 注入仓储服务接口
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepository(this IServiceCollection services) {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

    }
}
