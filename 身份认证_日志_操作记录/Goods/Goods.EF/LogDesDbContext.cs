using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Goods.EF
{
    public class LogDesDbContext : IDesignTimeDbContextFactory<LogDbContext>
    {
        public LogDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<LogDbContext> builder = new DbContextOptionsBuilder<LogDbContext>();
            builder.UseSqlServer("server=.;database=Goods;uid=sa;pwd=Lx141238792.");
            LogDbContext logDb = new LogDbContext(builder.Options);
            return logDb;
        }
    }
}
