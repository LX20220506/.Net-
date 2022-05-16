using Chat.IRepository;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Repository
{
    public class FriendRepository:BaseRepository<Friend>,IFriendRepository
    {
        public FriendRepository(ChatContext chatContext):base(chatContext) {
            
        }
    }
}
