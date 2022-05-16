using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Tools.Middleware
{
    public class SystemLogMiddleware
    {
        private readonly RequestDelegate next;

        public SystemLogMiddleware(RequestDelegate next) {
            this.next = next;
        }

        //public Task InvokeAsync(HttpContext httpContext) { 
        //    httpContext.
        //}
    }
}
