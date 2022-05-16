using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.IRepository;
using Chat.Models;

namespace Chat.Repository
{
    public class UserRepository:BaseRepository<UserInfo>,IUserRepository
    {
        public UserRepository(ChatContext chatContext) : base(chatContext) { 
            
        }
    }
}
