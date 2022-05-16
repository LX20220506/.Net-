using Goods.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goods.EF.Config
{
    public class LoginLogConfig : IEntityTypeConfiguration<LoginLog>
    {
        public void Configure(EntityTypeBuilder<LoginLog> builder)
        {
            builder.ToTable("LoginLog");
            builder.Property(l => l.IP).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            builder.Property(l => l.LoginState).IsRequired();
            builder.Property(l => l.UserName).HasColumnType("nvarchar");
            builder.Property(l => l.LoginTime).IsRequired();
        }
    }
}
