using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context;

public class PaymentConfigConfigurations : IEntityTypeConfiguration<PaymentConfig>
{
    public void Configure(EntityTypeBuilder<PaymentConfig> builder)
    {
        builder.ToTable("PaymentConfigurations");
        builder.Property(p => p.Type).HasMaxLength(25);
    }
}