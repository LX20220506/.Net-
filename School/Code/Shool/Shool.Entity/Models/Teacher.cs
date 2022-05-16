using SqlSugar;
using System;

namespace Shool.Entity.Models
{
    public class Teacher
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int TeacherId { get; set; }

        /// <summary>
        /// 老师工号
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(10)")]
        public string TeacherNo { get; set; }

        /// <summary>
        /// 老师姓名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(10)")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar(64)")]
        public string TeacherPwd { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int TeacherGender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime TeacherBirthday { get; set; }
    }
}
