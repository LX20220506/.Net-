using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shool.IRepository;
using Shool.Repository;
using Shool.Utility;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shool.JWT
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shool.JWT", Version = "v1" });
            });

            //注入 ORM
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = Configuration.GetConnectionString("ShoolDB"),
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            });

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();

            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapperHelper));

            //跨域
            services.AddCors(option =>
            {
                option.AddPolicy("MyCors", builder =>
                {
                    //builder.WithOrigins("https://localhost:44390", "http://0.0.0.0:3201").AllowAnyHeader();
                    builder//.WithOrigins("") // 允许部分站点跨域请求
                    //.AllowAnyOrigin() // 允许所有站点跨域请求（net core2.2版本后将不适用）
                    .SetIsOriginAllowed(o=>true)
                    .AllowAnyMethod() // 允许所有请求方法
                    .AllowAnyHeader() // 允许所有请求头
                    .AllowCredentials(); // 允许Cookie信息
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shool.JWT v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyCors");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
