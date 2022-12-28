using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(p => p.Id);
        builder.HasOne(p => p.Customer)
            .WithMany(u => u.Orders);
        builder.HasMany(order => order.Items)
            .WithMany(item => item.Orders);
        builder.HasOne(order => order.Cart)
            .WithOne(cart => cart.Order)
            .HasForeignKey<Order>(order => order.CartId);
    }
}