using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace BookAPI.Models
{
    public class BaseBook
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int ID { get; set; }
    }
}
