using SqlSugar;

namespace Shool.Entity.Models
{
    public class Success
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int SuccessId { get; set; }

        /// <summary>
        /// 课程id
        /// </summary>
        public int KeChengTypeId { get; set; }

        /// <summary>
        /// 学生学号
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// 成绩
        /// </summary>
        [SugarColumn(ColumnDataType = "decimal(5,1)")]
        public decimal ChengJi { get; set; }


        [SugarColumn(IsIgnore =true)]
        public KeChengType KeChengType { get; set; }
        [SugarColumn(IsIgnore =true)]
        public Student Student { get; set; }
    }
}
