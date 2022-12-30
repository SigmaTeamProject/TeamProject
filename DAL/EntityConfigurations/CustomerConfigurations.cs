using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DAL.Context;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers")
            .HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(25);
        builder.Property(p => p.Surname).HasMaxLength(25);
        builder.Property(p => p.Login).IsRequired();
        builder.Property(p => p.Password).IsRequired();
        builder.HasMany(p => p.Orders)
            .WithOne(u => u.Customer);
        builder.HasOne(p => p.PaymentConfig)
            .WithOne(p => p.Customer)
            .HasForeignKey<PaymentConfig>(p => p.CustomerId);
        builder.HasOne(p => p.Cart).WithOne(p => p.Customer).HasForeignKey<Cart>(p => p.CustomerId);

    }
}