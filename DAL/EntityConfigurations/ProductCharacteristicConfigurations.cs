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
        builder.HasKey(p => p.Id);
       // builder.Property(p => p.ProductId).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(25);
        builder.Property(p => p.Value).IsRequired();
        builder.HasOne(characteristic => characteristic.Product)
            .WithMany(product => product.Characteristics);
    }
}