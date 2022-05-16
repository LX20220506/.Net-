using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Models
{
    public class BooksInfo:BaseBook
    {
        public string BookName { get; set; }
        public string Writer { get; set; }
        public string Introduce { get; set; }
        public DateTime CreateDate { get; set; }
        public int TypeID { get; set; }


        public TypesInfo TypesInfo { get; set; }

    }
}
