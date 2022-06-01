using Demo.BingFa.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.BingFa.EF.Configs
{
    public class GoodsConfig : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> builder)
        {
            
            builder.ToTable("Goods");
            builder.Property(g=>g.GoodsNmae).HasMaxLength(20);
            builder.Property(g => g.Price).HasColumnType("decimal(8,2)");
            builder.Property(g => g.Stock);
            //builder.Ignore(g => g.Order);
        }
    }
}
