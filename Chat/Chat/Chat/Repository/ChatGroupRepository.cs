using Chat.IRepository;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Repository
{
    public class ChatGroupRepository:BaseRepository<ChatGroup>,ICharGroupRepository
    {
        public ChatGroupRepository(ChatContext chatContext) : base(chatContext) { 
            
        }
    }
}
