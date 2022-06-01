using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.Base.Redis
{
    public class RedisOptions
    {
        public string Configuration { get; set; }
        public string InstanceName { get; set; }
        public int DefaultDb { get; set; }
    }
}
