using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }

        [NotMapped]
        public int StudentId { get; set; }
        [NotMapped]
        public int CourseId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [Column(TypeName ="decimal(4,1)")]
        public decimal Score { get; set; }
    }
}
