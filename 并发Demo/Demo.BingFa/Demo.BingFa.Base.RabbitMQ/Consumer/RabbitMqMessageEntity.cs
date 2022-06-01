using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.BingFa.Base.RabbitMQ.Consumer
{
    public class RabbitMqMessageEntity
    {
        /// <summary>
        ///  消费事件
        /// </summary>
        public EventingBasicConsumer Consumer { get; set; }

        public BasicDeliverEventArgs BaseExceptionEvent { get; set; }

        //public IModel Channel { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否报错
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// 报错信息
        /// </summary>
        public string ErrorMesage { get; set; }

        /// <summary>
        /// 错误详情
        /// </summary>
        public Exception Exception { get; set; }
    }
}
