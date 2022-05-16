using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Goods.EF
{
    public class GoodsDbDesDbContext : IDesignTimeDbContextFactory<GoodsDbContext>
    {
        public GoodsDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<GoodsDbContext> builder = new DbContextOptionsBuilder<GoodsDbContext>();
            builder.UseSqlServer("server=.;database=Goods;uid=sa;pwd=Lx141238792.;");
            GoodsDbContext context = new GoodsDbContext(builder.Options);
            return context;
        }
    }
}
