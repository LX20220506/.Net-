using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public static class ResultHelper
    {
        public static Result Success(dynamic data) {
            return new Result() {
                Code=200,
                Msg="操作成功",
                Data=data,
                Total=0
            };
        }

        public static Result Success(dynamic data,int total) {
            return new Result() {
                Code = 200,
                Msg = "操作成功",
                Total=total,
                Data=data
            };
        }

        public static Result Error(string msg) {
            return new Result() {
                Code=500,
                Data=null,
                Total=0,
                Msg=msg
            };
        }
    }
}
