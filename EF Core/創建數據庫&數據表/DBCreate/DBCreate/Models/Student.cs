using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBCreate.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Column(TypeName ="nvarchar(20)")]
        public string StudentName { get; set; }
        public int Age { get; set; }

        [Column(TypeName ="nvarchar(5)")]
        public string Sex { get; set; }

        public int Score { get; set; }

        public int TId { get; set; }
    }
}
