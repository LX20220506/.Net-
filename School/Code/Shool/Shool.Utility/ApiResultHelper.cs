using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shool.Utility
{
    public static class ApiResultHelper
    {
        public static ApiResult Ok(dynamic obj) {
            return new ApiResult
            {
                Code = 200,
                Message = "ok",
                Obj = obj,
                Total = 0
            };
        }

        public static ApiResult Ok(dynamic obj,RefAsync<int> total) {
            return new ApiResult
            {
                Code = 200,
                Message = "ok",
                Obj = obj,
                Total = total
            };
        }

        public static ApiResult Err() {
            return new ApiResult
            {
                Code = 500,
                Message = "请求失败",
                Obj = null,
                Total = 0
            };
        }
        public static ApiResult Err(string emg)
        {
            return new ApiResult
            {
                Code = 500,
                Message = emg,
                Obj = null,
                Total = 0
            };
        }
    }
}
