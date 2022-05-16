using SqlSugar;
using System;

namespace Shool.Entity.Models
{
    public class Student
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int StudentId { get; set; }

        /// <summary>
        /// 学生学号
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(10)")]
        public string StudentNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(10)")]
        public string StudentName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar(64)")]
        public string StudentPwd { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int StudentGender { get; set; }


        /// <summary>
        /// 生日
        /// </summary>
        [SugarColumn(ColumnDataType = "date")]
        public DateTime StudentBirthday { get; set; }
    }
}
