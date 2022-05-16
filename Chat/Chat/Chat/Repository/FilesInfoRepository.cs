using Chat.IRepository;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Repository
{
    public class FilesInfoRepository : BaseRepository<FilesInfo>, IFilesInfoRepository
    {
        public FilesInfoRepository(ChatContext chatContext) : base(chatContext) { 
            
        }
    }
}
