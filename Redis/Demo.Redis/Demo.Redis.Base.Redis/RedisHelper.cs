using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Demo.Redis.Base.Redis
{
    public class RedisHelper
    {
        private readonly RedisOption _option;
        // 安装StackExchange.Redis包
        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> _connections;

        public RedisHelper(RedisOption option) {
            _option = option;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        // 连接Rebbit
        public ConnectionMultiplexer GetConnection() {
           return  _connections.GetOrAdd(_option.InstanceName, p =>ConnectionMultiplexer.Connect(_option.Configuration));
        }

        // 创建数据库
        public IDatabase GetDatabase() {
            return GetConnection().GetDatabase(_option.DefaultDb);
        }

        /// <summary>
        /// 获取字符串缓存
        /// </summary>
        /// <param name="key"></param>
        public string StringGet(string key) {
            try
            {
                return GetDatabase().StringGet(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 设置字符串缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>s
        public void StringSet(string key, dynamic value) {
            try
            {
                GetDatabase().StringSet(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expriy"></param>
        public void SetExpriy(string key,TimeSpan expriy) {
            try
            {
                GetDatabase().KeyExpire(key,expriy);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
