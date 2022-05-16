using Goods.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Goods.EF
{
    public class GoodsDbContext:DbContext
    {
        public DbSet<GoodsInfo> GoodsInfo { get; set; }

        public GoodsDbContext(DbContextOptions<GoodsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
