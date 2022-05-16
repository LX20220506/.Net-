using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class ChatGroup
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string ChatGroupName { get; set; }
        public int Count { get; set; }
        [Column(TypeName ="nvarchar(500)")]
        public string Member { get; set; }
    }
}
