using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shool.Entity.Dto
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }
        public string TeacherNo { get; set; }

        public string TeacherName { get; set; }

        public int TeacherGender { get; set; }
        public DateTime TeacherBirthday { get; set; }
    }
}
