using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_2.Models
{
    public class BookTypeInfo:BaseBook
    {
        public string BookName { get; set; }
        public string  Writer { get; set; }
        public string Sponsor { get; set; }
        public int TypeID { get; set; }

        public TypesInfo TypesInfo { get; set; }


    }
}
