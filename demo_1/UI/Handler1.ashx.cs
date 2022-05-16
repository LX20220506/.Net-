using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace UI
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string corpName = context.Request["corpName"];　　　　　　//接收前台传过来的参数
            if (corpName != "")
            {
                context.Response.Write(corpName);
                context.Response.End();
            } 


        }

        [WebMethod]
        public static string Show() {
            return "name";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}