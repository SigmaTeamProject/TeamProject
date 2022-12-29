using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(25);
        builder.HasMany(p => p.Characteristics)
            .WithOne(u => u.Product)
            .HasForeignKey(characteristic => characteristic.ProductId);
    }
}