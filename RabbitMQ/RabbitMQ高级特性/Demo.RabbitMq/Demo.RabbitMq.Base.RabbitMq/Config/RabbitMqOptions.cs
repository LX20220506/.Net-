namespace Demo.RabbitMq.Base.RabbitMq.Config
{
    public class RabbitMqOptions
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 虚拟主机名称
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

    }
}
