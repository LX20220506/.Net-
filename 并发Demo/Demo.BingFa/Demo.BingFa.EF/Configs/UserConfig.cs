using Demo.BingFa.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.BingFa.EF.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.UserName).HasMaxLength(20);
            builder.Property(u => u.UserPwd).HasColumnType("varchar").HasMaxLength(15);
            builder.Property(u => u.Email).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(u => u.Phone).HasColumnType("varchar").HasMaxLength(11);
            //builder.Ignore(u=>u.Orders);
        }
    }
}
