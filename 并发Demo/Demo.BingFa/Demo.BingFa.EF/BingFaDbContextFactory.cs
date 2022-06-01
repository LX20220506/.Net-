using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Demo.BingFa.EF
{
    public class BingFaDbContextFactory : IDesignTimeDbContextFactory<BingFaDbContext>
    {
        public BingFaDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlServer("server=.;database=Test_BingFa;uid=sa;pwd=Lx141238792.");

            return new BingFaDbContext(builder.Options);
        }
    }

}
