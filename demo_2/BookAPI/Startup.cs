using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using MyBook.IRepository;
using MyBook.Repository;
using MyBook.Service;
using MyBook.IService;

namespace BookAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();



            #region SqlSugarIOC的注入
            // 10秒入门
            SugarIocServices.AddSqlSugar(new IocConfig()
            {
                //ConfigId="db01"  多租户用到
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            }); ;
            #endregion


            #region 各個類的注入
            services = IOCExtend.AddSqlSugarIOC(services);
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class IOCExtend {
        public static IServiceCollection AddSqlSugarIOC(IServiceCollection services) {
            services.AddScoped<IBookInfoRepository, BookInfoRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ITypesInfoRepository, TypesInfoRepository>();
            services.AddScoped<ITypesInfoService, TypesInfoService>();
            services.AddScoped<IAdminInfoRepository, AdminInfoRepository>();
            services.AddScoped<IAdminInfoService, AdminInfoService>();
            return services;
        }
    }
}
