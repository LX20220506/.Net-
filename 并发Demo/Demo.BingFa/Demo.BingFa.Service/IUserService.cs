using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.Service
{
    public interface IUserService
    {
        Task CreateUserAsync(string userName,string pwd,string phone,string Email);
    }
}
