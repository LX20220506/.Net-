using Demo.Redis.Base.Redis;
using Demo.Redis.EF;
using Demo.Redis.Entity;
using Demo.Redis.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Demo.Redis.HttpApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestRedisController : ControllerBase
    {
        private readonly RedisHelper _redis;
        private readonly IRepositoryWrapper _repository;
        private readonly RedisTestDbContext _dbContext;


        public TestRedisController(RedisHelper redis, IRepositoryWrapper repository, RedisTestDbContext dbContext)
        {
            _redis = redis;
            _repository = repository;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Test() {
            _redis.StringSet("hello","123");
            string test = _redis.StringGet("hello");
            return Ok(test);
        }

        [HttpGet]
        public IActionResult GetAcctionById(int id) {
            string accountJson = _redis.StringGet("id:" + id);
            Account account;
            if (string.IsNullOrEmpty(accountJson))
            {
                Console.WriteLine("DB中拿");
                            
                account = _repository.UserRepository.FindAccountById(id);
                _redis.StringSet("id:"+account.Id,account);
                // 设置过期时间
                _redis.SetExpriy("id:" + account.Id,TimeSpan.FromSeconds(60));
            }
            else
            {
                Console.WriteLine("Redis中拿");
                account = JsonConvert.DeserializeObject<Account>(accountJson);
                // 设置滑动过期时间
                _redis.SetExpriy("id:" + account.Id, TimeSpan.FromSeconds(60));
            }
            return Ok(account); 
        }


        [HttpPost]
        public async Task<IActionResult>  CreateAccount(string name,string pwd) {
            Account account = new Account() { Name = name, Pwd = pwd };
            _repository.UserRepository.Create(account);
            await _repository.Save();
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateAccountTest1(string name, string pwd)
        {
            Account account = new Account() { Name = name, Pwd = pwd };
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return Ok();
        }


    }
}
