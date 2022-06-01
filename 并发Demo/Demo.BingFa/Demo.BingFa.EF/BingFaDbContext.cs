using Demo.BingFa.Entity;
using Microsoft.EntityFrameworkCore;

namespace Demo.BingFa.EF
{
    public class BingFaDbContext:DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BingFaDbContext(DbContextOptions options) : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
