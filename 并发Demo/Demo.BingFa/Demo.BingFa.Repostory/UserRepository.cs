using Demo.BingFa.EF;
using Demo.BingFa.Entity;
using Demo.BingFa.IRepository;

namespace Demo.BingFa.Repostory
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly BingFaDbContext _dbContext;

        public UserRepository(BingFaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
