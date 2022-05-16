using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }


        [Required]
        [Column(TypeName ="varchar(20)")]
        public string TeacherPwd { get; set; }


        [Required]
        [Column(TypeName ="nvarchar(10)")]  
        public string TeacherName { get; set; }


        public DateTime StartDate { get; set; }


        [Column(TypeName ="varchar(20)")]
        public string LastName { get; set; }


        public DateTime LastDateTime { get; set; }

        public Course Course { get; set; }
        [NotMapped]
        public int CourseId { get; set; }

    }
}
