using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.IRepository
{
    public interface IRepositoryWrapper
    {
        public IOrderRepository Order { get; }

        public IUserRepository User { get; }

        public IGoodsRepository Goods { get; }

        Task SaveAsync();
    }
}
