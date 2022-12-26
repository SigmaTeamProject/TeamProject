using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Price).IsRequired();
        builder.HasMany(p => p.Characteristics).WithOne(detail => detail.Product);
    }
}