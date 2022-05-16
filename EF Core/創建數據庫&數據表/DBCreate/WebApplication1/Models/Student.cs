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
    }
}
