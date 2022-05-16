using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Shool.API.AOP
{
    public class AOPDemo : DispatchProxy
    {
        public object ProxyClass { get; set; }// 代理类；通过赋值，拿到被代理的对象

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            //执行前操作

            //执行被代理的类 的请求(执行被代理对象的方法）
            targetMethod.Invoke(ProxyClass, args);

            //执行后操作
            return "";
        }
    }
}
