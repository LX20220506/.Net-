using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class FilesInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string FileName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FileUrl { get; set; }
        public DateTime UoloadDateTime { get; set; }


        public UserInfo UserInfo { get; set; }

    }
}
