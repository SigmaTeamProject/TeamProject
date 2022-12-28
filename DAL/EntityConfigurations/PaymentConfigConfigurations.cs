using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class PaymentConfigConfigurations : IEntityTypeConfiguration<PaymentConfig>
{
    public void Configure(EntityTypeBuilder<PaymentConfig> builder)
    {
        builder.ToTable("PaymentConfigurations");
        builder.HasKey(p => p.CustomerId);
        builder.Property(p => p.Type).HasMaxLength(25);
        builder.HasOne(paymentConfig => paymentConfig.Customer)
            .WithOne(customer => customer.PaymentConfig);
    }
}