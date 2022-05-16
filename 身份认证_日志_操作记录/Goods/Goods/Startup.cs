using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Goods.EF;
using Microsoft.AspNetCore.Identity;
using Goods.Entity.Models;
using Goods.Tools.Extend;
using System.Configuration;

namespace Goods
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Goods", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                    new OpenApiSecurityScheme{
                      Reference=new OpenApiReference{
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                      }
                    },
                    new string[] {}
                  }
                });
            });

            //services.AddLogging();
            #region 扩展方法

            // 添加Serlog服务
            services.AddSerilog();

            // 添加Action过滤器
            services.AddActionAsyncFilter();

            // 添加Jwt
            string client = Configuration.GetSection("JwtConfig").GetSection("Clients").Value;
            string[] clients = (client.Substring(0, client.Length - 1)).Split(',');

            services.AddJwt(clients, Configuration.GetSection("JwtConfig").GetSection("Key").Value);
            #endregion



            //配置数据库上下文对象
            services.AddDbContext<GoodsDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("Goods"));
            });
            services.AddDbContext<AuthorityDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("Goods"));
            });
            services.AddDbContext<LogDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("Goods"));
            });


            services.AddDataProtection();// 添加数据保护

            

            // 配置identity
            services.AddIdentityCore<User>(options=> {
                // 配置密码
                options.Password.RequireDigit = false;// 数字
                options.Password.RequiredLength = 6; // 长度
                options.Password.RequireLowercase = false;// 小写字母
                options.Password.RequireNonAlphanumeric = false;// 特殊符号
                options.Password.RequireUppercase = false;// 大写字母
            });

            IdentityBuilder builder = new IdentityBuilder(typeof(User),typeof(Role),services);
            builder.AddEntityFrameworkStores<AuthorityDbContext>() // 添加关联的数据库上下文对象
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<Role>>() // 添加用户管理器
                .AddUserManager<UserManager<User>>();// 添加角色管理器
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goods v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
