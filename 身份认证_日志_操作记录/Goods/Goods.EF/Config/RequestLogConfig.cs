using Goods.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goods.EF.Config
{
    public class RequestLogConfig : IEntityTypeConfiguration<RequestLog>
    {
        public void Configure(EntityTypeBuilder<RequestLog> builder)
        {
            builder.ToTable("RequestLog");
            builder.Property(r=>r.IP).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            builder.Property(r => r.RequestTime).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(r => r.StateCode).IsRequired();
            builder.Property(r => r.UserName).HasColumnType("nvarchar").HasMaxLength(10);
            builder.Property(r => r.ActionName).HasColumnType("varchar").HasMaxLength(40);
            builder.Property(r => r.ControllerName).HasColumnType("varchar").HasMaxLength(40);
            builder.Property(r => r.Method).HasColumnType("varchar").HasMaxLength(5);
            builder.Property(r => r.Parameter).HasColumnType("nvarchar").HasMaxLength(200);
            builder.Property(r => r.Url).HasColumnType("nvarchar").HasMaxLength(100);
        }
    }
}
