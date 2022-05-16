namespace Goods.Tools
{
    public class ApiResult
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StateCode { get; set; }

        /// <summary>
        /// 数据行数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic Data { get; set; }

        public ApiResult() { }

        public ApiResult( dynamic data=null, string msg = "", int stateCode = 200, int total = 0) {
            this.Message = msg;
            this.StateCode = stateCode;
            this.Total = total;
            this.Data = data;
        }
    }
}
