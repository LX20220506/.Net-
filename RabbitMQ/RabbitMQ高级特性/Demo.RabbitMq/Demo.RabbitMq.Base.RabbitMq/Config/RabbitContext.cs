using RabbitMQ.Client;

namespace Demo.RabbitMq.Base.RabbitMq.Config
{
    public class RabbitContext
    {
        private readonly RabbitMqOptions _rabbitMqOptions;
        private IConnection _conn = null;

        public RabbitContext(RabbitMqOptions rabbitMqOptions) {
            _rabbitMqOptions = rabbitMqOptions;
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public IConnection GetRabbitMqConnection() {
            if (_conn==null)
            {
                if (string.IsNullOrEmpty(_rabbitMqOptions.Address))
                {
                    var factory = new ConnectionFactory();
                    factory.HostName = _rabbitMqOptions.HostName;
                    factory.Port = _rabbitMqOptions.Port;
                    factory.UserName = _rabbitMqOptions.UserName;
                    factory.Password = _rabbitMqOptions.Password;
                    factory.VirtualHost = _rabbitMqOptions.VirtualHost;

                    _conn = factory.CreateConnection();
                }
            }

            return _conn;
        }
    }
}
