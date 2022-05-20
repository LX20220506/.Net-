using Demo.Redis.EF.Config;
using Demo.Redis.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Redis.EF
{
    public class RedisTestDbContext:DbContext
    {

        public DbSet<Account> Accounts { get; set; }

        public RedisTestDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                this.GetType().Assembly,
                predicate=> predicate.Namespace.Contains("Demo.Redis.EF.Config"));
        }
    }
}
