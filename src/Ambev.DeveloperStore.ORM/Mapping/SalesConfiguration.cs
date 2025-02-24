using Ambev.DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ambev.DeveloperStore.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.SaleNumber).IsRequired();
        builder.Property(u => u.SaleDate).IsRequired();
        builder.Property(u => u.BranchName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.CustomerName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.IsCancelled).IsRequired();
        builder.Property(u => u.TotalSaleAmount).IsRequired();

    }
}
