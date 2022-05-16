using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Microsoft.AspNetCore.Mvc;
using Goods.Tools.Filter;
using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Goods.Tools.Extend
{
    /// <summary>
    /// Service的扩展类
    /// </summary>
    public static class ServiceExtend
    {
        /// <summary>
        /// 添加Serilog服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSerilog(this IServiceCollection services) {
            // 添加logging服务
            services.AddLogging(logBulider => {

                var outputTemplate = "{NewLine}【{Level:u3}】{Timestamp:yyyy-MM-dd HH:mm:ss.fff}" +
                                     "{NewLine}#Msg# {Message:lj}" +
                                     "{NewLine}#Pro# {Properties:j}" +
                                     "{NewLine}#Exc# {Exception}{NewLine}{NewLine}";//输出模板

                // 创建Logger,绑定Serilog
                var log = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft",LogEventLevel.Information)
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File(
                        Path.Combine("Log", "_log" + DateTime.Now.ToString("yyMM") + ".log"),   // 文件存放路径和名称
                        restrictedToMinimumLevel:LogEventLevel.Information,                     // 最低输出日志级别
                        outputTemplate: outputTemplate,                                         // 输出模板
                        rollingInterval: RollingInterval.Month,                                 // 日志按日保存，这样会在文件名称后自动加上日期后缀   
                        //fileSizeLimitBytes: 1024^2*2,                                          // 文件大小，单文件建议2M，这里30M
                        encoding: Encoding.UTF8                                                 // 文件字符编码
                        )
                .WriteTo.File(
                        Path.Combine("Log", "_errLog.log"),   // 文件存放路径和名称
                        restrictedToMinimumLevel: LogEventLevel.Error,                     // 最低输出日志级别
                        outputTemplate: outputTemplate,                                         // 输出模板
                        rollingInterval: RollingInterval.Month,                                 // 日志按日保存，这样会在文件名称后自动加上日期后缀   
                        //fileSizeLimitBytes: 1024^2*2,                                          // 文件大小，单文件建议2M，这里30M
                        encoding: Encoding.UTF8                                                 // 文件字符编码
                        )
                .CreateLogger();

                logBulider.AddSerilog(log); // 添加 Serilog 服务

            });
            return services;
        }

        /// <summary>
        /// 扩展方法：添加ActionFilert
        /// </summary>
        public static IServiceCollection AddActionAsyncFilter(this IServiceCollection services) {

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ActionFilter>();
            });

            return services;
        }


        /// <summary>
        /// 扩展方法添加Jwt
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwt(this IServiceCollection services, string[] clients,string key) {

            // 配置鉴权
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options=> {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true, // 验证秘钥
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),// 秘钥

                        ValidateIssuer = true, // 验证发布者
                        ValidIssuer = "server", // 指定发布者

                        ValidateAudience = true, // 验证订阅者
                        ValidAudiences = clients, // 指定订阅者

                        ValidateLifetime = true, // 验证时间
                        ClockSkew = TimeSpan.FromMinutes(60) //获取或设置验证时间时要应用的时钟偏移

                    };
                });

            return services;
        }
    }
}
