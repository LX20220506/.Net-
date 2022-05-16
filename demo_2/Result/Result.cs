using System;

namespace Result
{
    public class Result
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public int Total { get; set; }
        public dynamic Data { get; set; }
    }
}
