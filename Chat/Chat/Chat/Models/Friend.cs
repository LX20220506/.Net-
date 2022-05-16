using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int FriendId { get; set; }

        [Column(TypeName ="nvarchar(30)")]
        public string Notes { get; set; }

        public UserInfo userInfo { get; set; }
    }
}
