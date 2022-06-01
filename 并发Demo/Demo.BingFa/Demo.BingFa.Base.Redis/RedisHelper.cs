using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace Demo.BingFa.Base.Redis
{
    public class RedisHelper
    {
        private readonly RedisOptions _redisOptions;
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabaseAsync _dbAsync;
        private readonly IDatabase _db;


        public RedisHelper(RedisOptions redisOptions)
        {
            _redisOptions = redisOptions;   // 传入redis的配置
            _connection = ConnectionMultiplexer.Connect(_redisOptions.Configuration);   // 打开连接
            _dbAsync = _connection.GetDatabase(_redisOptions.DefaultDb); // 连接到指定数据库
            _db = _connection.GetDatabase(_redisOptions.DefaultDb);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetExpiry(string key,TimeSpan expiry) {
            return await _dbAsync.KeyExpireAsync(key,expiry);
        }


        #region string

        public async Task<bool> IsKey(string key) {
            return await _dbAsync.KeyExistsAsync(key);
        }

        public async Task<string> StringGetAsync(string key) {
            return await _dbAsync.StringGetAsync(key);
        }

        public bool StringSetAsync(string key, string value, TimeSpan? expiry=default(TimeSpan?)) {
           // Console.WriteLine(value);
            return _db.StringSet(key,value,expiry);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key) {
            var value = await _dbAsync.StringGetAsync(key);
            if (string.IsNullOrEmpty(value))
            {
                return default; // default关键字返回默认值
            }
            return JsonConvert.DeserializeObject<T>(await _dbAsync.StringGetAsync(key));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            var value = _db.StringGet(key);
            if (string.IsNullOrEmpty(value))
            {
                return default; // default关键字返回默认值
            }
            return JsonConvert.DeserializeObject<T>( _db.StringGet(key));
        }

        public async Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?)) {
            string json = JsonConvert.SerializeObject(obj);
            return await _dbAsync.StringSetAsync(key,json,expiry);
        }

        public async Task DeleteKey(string key) {
            await _dbAsync.StringGetDeleteAsync(key);
        }

        #endregion

    }
}
