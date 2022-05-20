using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Redis.Base.Redis
{
    public class RedisOption
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Configuration { get; set; }
        
        /// <summary>
        /// 前置名
        /// </summary>
        public string InstanceName { get; set; }
        
        /// <summary>
        /// 要创创建的数据库名称（Redis中的数据库名称必须是数字）
        /// </summary>
        public int DefaultDb { get; set; }
    }
}
