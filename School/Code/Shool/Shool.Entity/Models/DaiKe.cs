using SqlSugar;

namespace Shool.Entity.Models
{
    public class DaiKe
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int DaiKeId { get; set; }

        /// <summary>
        /// 老师id
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 课程id
        /// </summary>
        public int KeChengTypeId { get; set; }


        [SugarColumn(IsIgnore =true)]
        public Teacher Teacher { get; set; }
        [SugarColumn(IsIgnore = true)]
        public KeChengType KeChengType { get; set; }
    }
}
