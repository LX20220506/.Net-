using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Demo.Redis.EF
{
    public class RedisTestDbContextDesignFactory : IDesignTimeDbContextFactory<RedisTestDbContext>
    {
        public RedisTestDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=RedisTest;Integrated Security=True");

            return new RedisTestDbContext(optionsBuilder.Options);
        }
    }
}
