using System;

namespace Shool.Entity.Dto
{
    public class StudentDto
    {
        public int StudentId { get; set; }

        public string StudentNo { get; set; }

        public string StudentName { get; set; }

        public int StudentGender { get; set; }

        public DateTime StudentBirthday { get; set; }
    }
}
