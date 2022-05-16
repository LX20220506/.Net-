using Goods.EF;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json;
using Goods.Entity.Models;

namespace Goods.Tools.Filter
{
    public class ActionFilter : IAsyncActionFilter
    {
        private readonly LogDbContext _logDbContext;

        public ActionFilter(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 获取控制器路由信息
            var actionDescriptor = context.ActionDescriptor  as ControllerActionDescriptor;
            
            // 获取请求信息
            var httpContext = context.HttpContext;
            var request = httpContext.Request;

            // 获取输入的参数；参数是字典类型，将它转换成list类型
            var requestData = context.ActionArguments.ToList();

            List<object> list = new List<object>();
            foreach (var item in requestData)
            {
                list.Add(item.Value);// 将字典中的值，添加到一个新的list中
            }
            // 参数
            string parameter = JsonConvert.SerializeObject(list);// 将所有的值序列化为json格式，得到所有的参数

            // 控制器名称
            string controllersName = actionDescriptor.ControllerName;

            // action名称
            string actionName = actionDescriptor.ActionName;

            // HTTP动词
            string method = request.Method;

            // 请求的url
            string url = request.Host + request.Path;
            
            // IP地址
            string IP = httpContext.Connection.RemoteIpAddress.ToString();
           
            // 请求时间
            var requestedTime = DateTimeOffset.Now;

            // 用户名
            string userName = httpContext.User.Identity.Name;

            // 开启事务
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var result = await next.Invoke();
            

            // 添加日志
            if (actionName=="Login")
            {
                await _logDbContext.LoginLog.AddAsync(new LoginLog
                {
                    IP = IP,
                    UserName = userName,
                    LoginState = httpContext.Response.StatusCode,
                    LoginTime = DateTime.Now
                }) ;
            }
            else
            {
                // 将请求的过程存入数据库中
                await _logDbContext.RequestLog.AddAsync(new RequestLog()
                {
                    ActionName = actionName,
                    ControllerName = controllersName,
                    IP = IP,
                    Method = method,
                    Parameter = parameter,
                    RequestTime = (DateTimeOffset.Now - requestedTime).ToString(),
                    StateCode = httpContext.Response.StatusCode,
                    UserName = userName
                });
            }
            await _logDbContext.SaveChangesAsync();

            if (result.Exception == null)
            {
                // 提交事务
                transactionScope.Complete();
            }

        }
    }
}
