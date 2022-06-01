namespace Demo.BingFa.Base.RabbitMQ.Config
{
    public class RabbitMqOptions
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Prot { get; set; }

        /// <summary>
        /// 虚拟主机
        /// </summary>
        public string VirtualHost { get; set; }
    }
}
