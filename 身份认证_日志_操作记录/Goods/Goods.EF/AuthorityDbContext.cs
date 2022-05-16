using Goods.Entity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goods.EF
{
    public class AuthorityDbContext:IdentityDbContext<User,Role,int>
    {
        public AuthorityDbContext(DbContextOptions<AuthorityDbContext> options) : base(options) { }
    }
}
