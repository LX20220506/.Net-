using Demo.Redis.Entity;
using Microsoft.EntityFrameworkCore;

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
