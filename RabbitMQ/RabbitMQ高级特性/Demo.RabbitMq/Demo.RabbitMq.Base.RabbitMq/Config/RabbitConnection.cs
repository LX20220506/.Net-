using RabbitMQ.Client;

namespace Demo.RabbitMq.Base.RabbitMq.Config
{
    public class RabbitConnection
    {
        private readonly RabbitMqOptions _rabbitMqOptions; // 配置信息
        private IConnection _conn = null; // 连接器

        // 在构造函数中加载配置
        public RabbitConnection(RabbitMqOptions rabbitMqOptions) {
            _rabbitMqOptions = rabbitMqOptions;
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public IConnection GetRabbitMqConnection() {
            if (_conn==null)
            {
				// 沒有添加Address,这个RabbitMQ集群中用到的
                if (string.IsNullOrEmpty(_rabbitMqOptions.Address))
                {
                    // 创建连接工厂
                    var factory = new ConnectionFactory();
                    factory.HostName = _rabbitMqOptions.HostName;
                    factory.Port = _rabbitMqOptions.Port;
                    factory.UserName = _rabbitMqOptions.UserName;
                    factory.Password = _rabbitMqOptions.Password;
                    factory.VirtualHost = _rabbitMqOptions.VirtualHost;

                    // 创建连接器
                    _conn = factory.CreateConnection(); 
                }
            }

            // 返回连接器
            return _conn; 
        }
    }
}
