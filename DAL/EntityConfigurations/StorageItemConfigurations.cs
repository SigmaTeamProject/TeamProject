using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class StorageItemConfigurations
{
    public void Configure(EntityTypeBuilder<StorageItem> builder)
    {
        builder.ToTable("StorageItems");
        builder.HasKey(p => p.ProductId);
    }
}