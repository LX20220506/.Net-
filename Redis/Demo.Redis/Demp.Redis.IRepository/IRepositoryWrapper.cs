using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Redis.IRepository
{
    public interface IRepositoryWrapper
    {
        public IUserRepository UserRepository { get; }

        Task Save();
    }
}
