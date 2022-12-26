using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class ProductCharacteristicConfigurations : IEntityTypeConfiguration<ProductCharacteristic>
{
    public void Configure(EntityTypeBuilder<ProductCharacteristic> builder)
    {
        builder.ToTable("ProductCharacteristics");
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(25);
        builder.HasOne(p => p.Product).WithMany(u => u.Characteristics);
        builder.Property(p => p.Value).IsRequired();
    }
}