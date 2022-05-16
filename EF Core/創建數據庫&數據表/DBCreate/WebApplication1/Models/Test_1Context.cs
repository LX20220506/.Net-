using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBCreate.Models;

namespace DBCreate.Models
{
    public class Test1Context:DbContext
    {
        public Test1Context(DbContextOptions option):base(option) 
        { 
        
        }

        public DbSet<Student> Students { get; set; }
    }
}
