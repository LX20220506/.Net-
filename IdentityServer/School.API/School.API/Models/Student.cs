using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName ="varchar(20)")] 
        public string StudentPwd { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(10)")]
        public string StudentName { get; set; }

        public int Age { get; set; }

        [Column(TypeName ="int")]
        public Gender Sex { get; set; }//性别

        public DateTime StartDate { get; set; }//入学时间

        [Column(TypeName ="varchar(20)")]
        public string LastName { get; set; }//最后登录用户

        public DateTime LastDateTime { get; set; }//最后登录时间
        
    }
    public enum Gender { Boy = 1, Girl = 0 };
}
