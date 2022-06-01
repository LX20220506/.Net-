using System;
using System.Collections.Generic;

namespace Demo.BingFa.Base.RabbitMQ.Consumer
{
    public class QueueInfo
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 交换机类型
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// 定义属性
        /// </summary>
        public IDictionary<string,object> Props { get; set; }

        /// <summary>
        /// 用来接受外部的业务代码
        /// </summary>
        public Action<RabbitMqMessageEntity> OnReceivedCallback { get; set; }

    }
}
