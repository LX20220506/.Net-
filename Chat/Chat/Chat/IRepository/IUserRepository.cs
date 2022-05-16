using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;

namespace Chat.IRepository
{
    public interface IUserRepository:IBaseRepository<UserInfo>
    {
    }
}
