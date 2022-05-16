using Castle.DynamicProxy;
using System;

namespace Shool.API.AOP
{
    public class AOPDemo2 : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("方法执行前");

            invocation.Proceed();

            Console.WriteLine("方法执行后");
        }
    }
}