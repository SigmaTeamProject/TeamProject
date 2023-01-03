using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class StorageItemConfigurations: IEntityTypeConfiguration<StorageItem>
{
    public void Configure(EntityTypeBuilder<StorageItem> builder)
    {
        builder.ToTable("StorageItems").HasKey(p=>p.ProductId);
        builder.HasOne(item => item.Product)
            .WithOne(product => product.StorageItem)
            .HasForeignKey<StorageItem>(item => item.ProductId);
    }
}