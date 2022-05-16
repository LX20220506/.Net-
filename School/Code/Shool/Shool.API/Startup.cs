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
using Shool.Entity.Models;
using SqlSugar.IOC;
using Shool.IRepository;
using Shool.Repository;
using Shool.IService;
using Shool.Service;
using Shool.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using Shool.API.AOP;
using Castle.DynamicProxy;

namespace Shool.API
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

            // 配置Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shool.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    BearerFormat = "JWT",//格式
                    Scheme = "Bearer"//计划
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { 
                        new OpenApiSecurityScheme(){ 
                            Reference=new OpenApiReference{ 
                                Id="Bearer",
                                Type=ReferenceType.SecurityScheme
                            }
                        },
                        new string[]{ }
                    }
                });

            });

            //注入 ORM
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = Configuration.GetConnectionString("ShoolDB"),
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            });

            // 依赖注入
            services.AddRepositoryAndService();

            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapperHelper));

            //JWT
            services.AddJWT();

            

            // 添加授权策略 
            services.AddAuthorization(o => {
            //授权策略的几种方式：
                //1.角色策略授权，在claim中定义一个角色，在认证时会判断该角色是否有权限访问API
                o.AddPolicy("student", policy =>
                {
                    //这个添加的是或(or)关系  一个用户是admin 或者是 teacher 再或者是 student
                    policy.RequireRole("admin", "teacher", "student").Build();
                });

                o.AddPolicy("teacher", policy =>
                {
                    policy.RequireRole("admin", "teacher").Build();
                });

                o.AddPolicy("admin", policy =>
                {
                    policy.RequireRole("admin").Build();
                });

                //o.AddPolicy("and", policy =>
                //{
                // 这里添加的是于(and)关系，即一个用户是 role1 也是 role2
                //    policy.RequireRole("role1").RequireRole("role2").Build();
                //});

                //2.claim策略授权

                //3.复杂策略
                // 老张只提了一下，没有细讲，有点复杂

            });

            //跨域
            services.AddCors(option =>
            {
                option.AddPolicy("MyCors", builder =>
                {
                    builder
                    //.WithOrigins( "http://localhost:8080")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()//允许任何标头
                    .AllowAnyMethod();//允许任何方法
                });
            });


            #region AOP Deom
            // AOP 动态代理 原理
            {
                // DispatchProxy.Create 帮我们创建了 IAOPDemoService 和 AOPDemo 的实例
                // 创建完成之后 AOPDemo 成为 IAOPDemoService的代理；
                // 也就是说 AOPDemo 完全控制了 IAOPDemoService 的执行。
                // 现在是 AOPDemo 包含 IAOPDemoService 的关系
                //IAOPDemoService decorator = DispatchProxy.Create<IAOPDemoService, AOPDemo>();

                // 因为 AOPDemo 是 IAOPDemoService 的代理，
                // 所以将 IAOPDemoService对象 转换成 AOPDemo并不报错 ---- ((AOPDemo)decorator)
                // ProxyClass属性是指 IAOPDemoService的实例 （在代理中需要被代理的类 的实例，来调用被代理的类中的成员[方法、属性等]） 
                //((AOPDemo)decorator).ProxyClass = new AOPDemoService();

                // 此时，执行被代理的类的方法时，
                // 方法并不会执行，而是会进入到代理中，
                // 由代理来操作 是否执行 如何执行 等 
                //decorator.Test();

            }

            // AOP 动态代理 实际操作
            {
                // Assembly.Load() 会找到 Shool.Service 下的dll文件
                //Assembly assembly = Assembly.Load("Shool.Service");
                //List<Type> ts = assembly.GetTypes().Where(d => d.Name.EndsWith("Shool.Service")).ToList();

                //foreach (var item in ts.Where(s => !s.IsInterface))
                //{
                //    var interfaceType = item.GetInterfaces();
                //    foreach (var typeArray in interfaceType)
                //    {
                //        //services.AddScoped(typeArray, item);

                //        services.AddScoped(item);
                //        services.AddScoped<AOPDemo2>();
                //        services.AddScoped(provider =>
                //        {
                //            var generator = new ProxyGenerator();
                //            var target = provider.GetService(item);
                //            var interceptor = provider.GetService<AOPDemo2>();
                //            var proxy = generator.CreateInterfaceProxyWithTarget(typeArray, target, interceptor);

                //            return (IAOPDemoService)proxy;
                //        });
                //    }
                //}

                //services.AddScoped<AOPDemoService>();
                //services.AddScoped<AOPDemo2>();
                //services.AddScoped(provider=> {
                //    var generator = new ProxyGenerator();
                //    var terget = provider.GetService<AOPDemoService>();
                //    var interceptor = provider.GetService<AOPDemo2>();
                //    var proxy = generator.CreateInterfaceProxyWithTarget<IAOPDemoService>(terget,interceptor);
                //    return proxy;
                //});

            }
            #endregion

        }

        // This method gets called by thmhe runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shool.API v1"));
            }
            // 路由
            app.UseRouting();

            // 认证
            app.UseAuthentication();
            // 授权
            app.UseAuthorization();
            // 跨域
            app.UseCors("MyCors");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    public static class KuoZhan
    {

        public static IServiceCollection AddRepositoryAndService(this IServiceCollection services) {
            
            services.AddScoped<IDaiKeRepository,DaiKeRepository>();
            services.AddScoped<IDaiKeService, DaiKeService>();

            services.AddScoped<IKeChenTypeRepository, KeChengRepository>();
            services.AddScoped<IKeChenTypeService, KeChenTypeService>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<ISuccessRepository, SuccessRepository>();
            services.AddScoped<ISuccessService, SuccessService>();

            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherService, TeacherService>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminServic, AdminService>();

            return services;
        }


        public static IServiceCollection AddJWT(this IServiceCollection services) {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5001",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(60),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dashk-asdkjakfbj-ADJlsdD78d-Dkd8Dd8"))//颁发者签名密钥
                };
            });

            return services;
        }
    }

}
