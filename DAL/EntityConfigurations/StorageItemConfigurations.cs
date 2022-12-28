using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class StorageItemConfigurations: IEntityTypeConfiguration<StorageItem>
{
    public void Configure(EntityTypeBuilder<StorageItem> builder)
    {
        builder.ToTable("StorageItems").HasKey(p=>p.ProductId);
        builder.HasMany(item => item.Carts)
            .WithMany(cart => cart.Items);
        builder.HasMany(item => item.Orders)
            .WithMany(order => order.Items);
        builder.ToTable("StorageItems");
        builder.HasKey(p=>p.Id);
    }
}