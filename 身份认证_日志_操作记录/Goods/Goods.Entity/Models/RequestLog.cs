using System;

namespace Goods.Entity.Models
{
    public class RequestLog
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 请求响应的用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// action名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 请求的IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public string RequestTime { get; set; }

        /// <summary>
        /// 响应状态码
        /// </summary>
        public int StateCode { get; set; }

        // HTTP请求动词
        public string Method { get; set; }

        // 请求的参数
        public string Parameter { get; set; }

        // 请求的地址
        public string Url { get; set; }

        // 请求时间
        public DateTime DateTime { get; set; }

    }
}
