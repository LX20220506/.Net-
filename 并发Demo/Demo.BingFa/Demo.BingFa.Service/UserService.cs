using Demo.BingFa.Entity;
using Demo.BingFa.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }
        public async Task CreateUserAsync(string userName, string pwd, string phone, string email)
        {
            _repositoryWrapper.User.Create(new User() {UserName=userName,UserPwd=pwd,Phone=phone,Email=email });
            await _repositoryWrapper.SaveAsync();
        }
    }
}
