using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.API.Models;

namespace School.API.Models
{
    public static class SeedData
    {
        public static void Initialize(SchoolContext context) {
            
            #region 添加学生类     
            
            context.Students.AddRangeAsync(
                new Student{
                    StudentId = 1001,
                    StudentName = "学生1",
                    StudentPwd = "123456",
                    Sex = Gender.Boy,
                    StartDate = DateTime.Parse("2021/9/1"),
                    Age=18,
                    LastDateTime=DateTime.Now,
                    LastName="1001"
                },
                new Student
                {
                    StudentId = 1002,
                    StudentName = "学生2",
                    StudentPwd = "123456",
                    Sex = Gender.Boy,
                    StartDate = DateTime.Parse("2021/9/1"),
                    Age = 18,
                    LastDateTime = DateTime.Now,
                    LastName = "1002"
                },
                new Student
                {
                    StudentId = 1003,
                    StudentName = "学生3",
                    StudentPwd = "123456",
                    Sex = Gender.Boy,
                    StartDate = DateTime.Parse("2021/9/1"),
                    Age = 18,
                    LastDateTime = DateTime.Now,
                    LastName = "1003"
                },
                new Student
                {
                    StudentId = 1004,
                    StudentName = "学生4",
                    StudentPwd = "123456",
                    Sex = Gender.Boy,
                    StartDate = DateTime.Parse("2021/9/1"),
                    Age = 18,
                    LastDateTime = DateTime.Now,
                    LastName = "1004"
                },
                new Student
                {
                    StudentId = 1005,
                    StudentName = "学生5",
                    StudentPwd = "123456",
                    Sex = Gender.Boy,
                    StartDate = DateTime.Parse("2021/9/1"),
                    Age = 18,
                    LastDateTime = DateTime.Now,
                    LastName = "1005"
                },
                new Student
                {
                    StudentId = 1006,
                    StudentName = "学生6",
                    StudentPwd = "123456",
                    Sex = Gender.Boy,
                    StartDate = DateTime.Parse("2021/9/1"),
                    Age = 18,
                    LastDateTime = DateTime.Now,
                    LastName = "1006"
                },
                new Student
                {
                    StudentId = 1007,
                    StudentName = "学生7",
                    StudentPwd = "123456",
                    Sex = Gender.Boy,
                    StartDate = DateTime.Parse("2021/9/1"),
                    Age = 18,
                    LastDateTime = DateTime.Now,
                    LastName = "1007"
                }
                );
            #endregion

            #region 添加课程数据
            context.Courses.AddRangeAsync(
                new Course
                {
                    CourseId = 1,
                    CouresName = "高数",
                }, new Course
                {
                    CourseId = 2,
                    CouresName = "英语",
                }, new Course
                {
                    CourseId = 3,
                    CouresName = "形势与政策",
                }, new Course
                {
                    CourseId = 4,
                    CouresName = "体育",
                }, new Course
                {
                    CourseId = 5,
                    CouresName = "心理健康",
                }, new Course
                {
                    CourseId = 6,
                    CouresName = "美术鉴赏",
                }
                ) ;
            #endregion

            #region 添加老师数据
            context.Teachers.AddRangeAsync(
                new Teacher
                {
                    TeacherId = 1001,
                    TeacherPwd = "123456",
                    TeacherName = "老师1",
                    CourseId=1,
                    StartDate=DateTime.Parse("2010/8/1"),
                    LastName= "1001",
                    LastDateTime=DateTime.Now
                },
                new Teacher
                {
                    TeacherId = 1002,
                    TeacherPwd = "123456",
                    TeacherName = "老师2",
                    CourseId = 2,
                    StartDate = DateTime.Parse("2010/8/1"),
                    LastName = "1002",
                    LastDateTime = DateTime.Now
                },
                new Teacher
                {
                    TeacherId = 1003,
                    TeacherPwd = "123456",
                    TeacherName = "老师3",
                    CourseId = 3,
                    StartDate = DateTime.Parse("2010/8/1"),
                    LastName = "1003",
                    LastDateTime = DateTime.Now
                },
                new Teacher
                {
                    TeacherId = 1004,
                    TeacherPwd = "123456",
                    TeacherName = "老师4",
                    CourseId = 4,
                    StartDate = DateTime.Parse("2010/8/1"),
                    LastName = "1004",
                    LastDateTime = DateTime.Now
                },
                new Teacher
                {
                    TeacherId = 1005,
                    TeacherPwd = "123456",
                    TeacherName = "老师5",
                    CourseId = 5,
                    StartDate = DateTime.Parse("2010/8/5"),
                    LastName = "1005",
                    LastDateTime = DateTime.Now
                },
                new Teacher
                {
                    TeacherId = 1006,
                    TeacherPwd = "123456",
                    TeacherName = "老师6",
                    CourseId = 6,
                    StartDate = DateTime.Parse("2010/8/6"),
                    LastName = "1006",
                    LastDateTime = DateTime.Now
                }
                );
            #endregion

            #region 添加成绩单数据
            context.Grade.AddRangeAsync(
                new Grade { 
                    GradeId=2021001,
                    CourseId=1,
                    StudentId=1001,
                    Score=80
                },
                new Grade
                {
                    GradeId = 2021002,
                    CourseId = 2,
                    StudentId = 1001,
                    Score = 87
                },
                new Grade
                {
                    GradeId = 2021003,
                    CourseId = 3,
                    StudentId = 1001,
                    Score = 55
                },
                new Grade
                {
                    GradeId = 2021004,
                    CourseId = 4,
                    StudentId = 1001,
                    Score = 90
                },
                new Grade
                {
                    GradeId = 2021005,
                    CourseId = 5,
                    StudentId = 1001,
                    Score = 85
                },
                new Grade
                {
                    GradeId = 2021006,
                    CourseId = 6,
                    StudentId = 1001,
                    Score = 86
                },



                new Grade
                {
                    GradeId = 2021007,
                    CourseId = 1,
                    StudentId = 1002,
                    Score = 85
                },
                new Grade
                {
                    GradeId = 2021008,
                    CourseId = 2,
                    StudentId = 1002,
                    Score = 99
                },
                new Grade
                {
                    GradeId = 2021009,
                    CourseId = 3,
                    StudentId = 1002,
                    Score = 79
                },
                new Grade
                {
                    GradeId = 2021010,
                    CourseId = 4,
                    StudentId = 1002,
                    Score =79
                },
                new Grade
                {
                    GradeId = 2021011,
                    CourseId = 5,
                    StudentId = 1002,
                    Score = 74
                },
                new Grade
                {
                    GradeId = 2021012,
                    CourseId = 6,
                    StudentId = 1002,
                    Score = 83
                },



                new Grade
                {
                    GradeId = 2021013,
                    CourseId = 1,
                    StudentId = 1003,
                    Score = 85
                },
                new Grade
                {
                    GradeId = 2021014,
                    CourseId = 2,
                    StudentId = 1003,
                    Score = 99
                },
                new Grade
                {
                    GradeId = 2021015,
                    CourseId = 3,
                    StudentId = 1003,
                    Score = 79
                },
                new Grade
                {
                    GradeId = 2021016,
                    CourseId = 4,
                    StudentId = 1003,
                    Score = 79
                },
                new Grade
                {
                    GradeId = 2021017,
                    CourseId = 5,
                    StudentId = 1003,
                    Score = 74
                },
                new Grade
                {
                    GradeId = 2021018,
                    CourseId = 6,
                    StudentId = 1003,
                    Score = 83
                },


                new Grade
                {
                    GradeId = 2021019,
                    CourseId = 1,
                    StudentId = 1004,
                    Score = new Random().Next(40,100)
                },
                new Grade
                {
                    GradeId = 2021020,
                    CourseId = 2,
                    StudentId = 1004,
                    Score = new Random().Next(40, 100)
                },
                new Grade
                {
                    GradeId = 2021021,
                    CourseId = 3,
                    StudentId = 1004,
                    Score = new Random().Next(40, 100)
                },
                new Grade
                {
                    GradeId = 2021022,
                    CourseId = 4,
                    StudentId = 1004,
                    Score = new Random().Next(40, 100)
                },
                new Grade
                {
                    GradeId = 2021023,
                    CourseId = 5,
                    StudentId = 1004,
                    Score = new Random().Next(40, 100)
                },
                new Grade
                {
                    GradeId = 2021024,
                    CourseId = 6,
                    StudentId = 1004,
                    Score = new Random().Next(40, 100)
                }

                );


            #endregion

        }
    }
}
