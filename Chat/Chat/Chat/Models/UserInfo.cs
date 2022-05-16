using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string UserName { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string UserPwd { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        public string HeadImg { get; set; }

    }
}
