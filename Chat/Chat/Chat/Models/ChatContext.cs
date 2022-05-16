using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Models;

namespace Chat.Models
{
    public class ChatContext:DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) :base(options)
        { 
        
        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<FilesInfo> FileInfo { get; set; }
        public DbSet<ChatGroup> ChatGroup { get; set; }
        public DbSet<Friend> Friend { get; set; }
    }
}
