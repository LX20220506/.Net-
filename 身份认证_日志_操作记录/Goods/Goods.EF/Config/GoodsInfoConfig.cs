using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goods.EF.Config
{
    public class GoodsInfoConfig : IEntityTypeConfiguration<GoodsInfo>
    {
        public void Configure(EntityTypeBuilder<GoodsInfo> builder)
        {
            builder.ToTable("Goods");
            builder.Property(g => g.GoodsName).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(g => g.GoodsPrice).HasColumnType("money").HasMaxLength(8).IsRequired();
            builder.Property(g => g.GoodsDesc).HasColumnType("text").HasMaxLength(16).IsRequired();
        }
    }
}
