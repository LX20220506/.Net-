using Demo.Redis.EF;
using Demo.Redis.Entity;
using Demo.Redis.IRepository;
using System.Linq;

namespace Demo.Redis.Repository
{
    public class UserRepository:RepositoryBase<Account>,IUserRepository
    {
        private readonly RedisTestDbContext _dbContext;

        public UserRepository(RedisTestDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 通过id查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account FindAccountById(int id) {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }
    }
}
