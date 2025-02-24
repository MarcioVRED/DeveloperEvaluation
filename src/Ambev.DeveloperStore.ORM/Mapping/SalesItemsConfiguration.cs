using Ambev.DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperStore.ORM.Mapping;

public class SalesItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SalesItems");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.SaleId).IsRequired();
        builder.Property(u => u.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Quantity).IsRequired();
        builder.Property(u => u.UnitPrice).IsRequired();
        builder.Property(u => u.Discount);
        builder.Property(u => u.IsCancelled).IsRequired();
        builder.Property(u => u.TotalItemAmount).IsRequired();
    }
}
