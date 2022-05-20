using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Redis.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Redis.EF.Config
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");
            builder.Property(a => a.Name).HasMaxLength(20).IsRequired();
            builder.Property(a => a.Pwd).HasColumnType("varchar").HasMaxLength(20).IsRequired();
        }
    }
}
