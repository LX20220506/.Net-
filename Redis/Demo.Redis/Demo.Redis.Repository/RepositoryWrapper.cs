using Demo.Redis.EF;
using Demo.Redis.IRepository;
using System.Threading.Tasks;

namespace Demo.Redis.Repository
{
    // 一个对外的类，将所有的仓储接口全部放到这里，到时候只用注入这个类，那么所有的仓储接口都可以使用
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RedisTestDbContext _dbContext;
        private IUserRepository _userRepository;


        public IUserRepository UserRepository // 判断_userRepository是否初始化，若没有则进行初始化
        {
            get
            {
                return _userRepository ??= new UserRepository(_dbContext);
            }
        }

        public RepositoryWrapper(RedisTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Save()
        {

             await _dbContext.SaveChangesAsync();
        }
    }
}
