using Goods.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Goods.EF
{
    public class LogDbContext:DbContext
    {
        public DbSet<LoginLog> LoginLog { get; set; }
        public DbSet<RequestLog> RequestLog { get; set; }

        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
