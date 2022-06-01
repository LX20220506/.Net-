using Demo.BingFa.EF;
using Demo.BingFa.IRepository;
using System.Threading.Tasks;

namespace Demo.BingFa.Repostory
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BingFaDbContext _dbContext;
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;
        private IGoodsRepository _goodsRepository;

        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                {
                    return new OrderRepostory(_dbContext);
                }
                return _orderRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_orderRepository == null)
                {
                    return new UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }

        public IGoodsRepository Goods
        {
            get
            {
                if (_orderRepository == null)
                {
                    return new GoodsRepository(_dbContext);
                }
                return _goodsRepository;
            }
        }

        public RepositoryWrapper(BingFaDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
