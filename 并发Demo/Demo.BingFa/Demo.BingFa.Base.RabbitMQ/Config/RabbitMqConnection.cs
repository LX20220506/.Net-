using RabbitMQ.Client;

namespace Demo.BingFa.Base.RabbitMQ.Config
{
    public class RabbitMqConnection
    {
        private readonly RabbitMqOptions _options;
        private IConnection _connection; // 单例模式

        public RabbitMqConnection(RabbitMqOptions options)
        {
            this._options = options;
        }


        /// <summary>
        /// 配置RabbitMQ并建立连接
        /// </summary>
        /// <returns></returns>
        public IConnection  GetConnection() {
            if (_connection==null)
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = _options.UserName;
                factory.Password = _options.Password;
                factory.HostName = _options.HostName;
                factory.Port = _options.Prot;
                factory.VirtualHost = _options.VirtualHost;

                _connection = factory.CreateConnection();
                factory.AutomaticRecoveryEnabled = true;//设置端口后自动恢复连接属性即可
            }
            return _connection; // 单例模式
        }
    }
}
