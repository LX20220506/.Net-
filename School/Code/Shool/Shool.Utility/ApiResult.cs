using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shool.Utility
{
    public class ApiResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public dynamic Obj { get; set; }

        public int Total { get; set; }
    }
}
