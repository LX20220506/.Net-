using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.EF
{
    public class AuthorityDesDbcontext : IDesignTimeDbContextFactory<AuthorityDbContext>
    {
        public AuthorityDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AuthorityDbContext> builder = new DbContextOptionsBuilder<AuthorityDbContext>();
            builder.UseSqlServer("server=.;database=Goods;uid=sa;pwd=Lx141238792.;");
            AuthorityDbContext dbContext = new AuthorityDbContext(builder.Options);
            return dbContext;
        }
    }
}
