using Demo.BingFa.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.BingFa.EF.Configs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(o => o.GoodsId);
            builder.Property(o => o.State);
            //builder.Ignore(o => o.User); // 忽略user
            //builder.Ignore(o=>o.Goods); // 忽略goods
            //builder.HasOne(o => o.Goods).WithMany(g => g.Order);
            //builder.HasOne(o => o.User).WithMany(u => u.Orders);
        }
    }
}
