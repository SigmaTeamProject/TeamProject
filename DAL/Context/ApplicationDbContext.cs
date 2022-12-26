using Data;
using Microsoft.EntityFrameworkCore;
using DAL.SeedData;

namespace DAL.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Check> Checks { get; set; }
    public DbSet<StorageItem> StorageItems { get; set; }
    public Customer Customers { get; set; }
    public PaymentConfig PaymentConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());

        SeedData.Seed(modelBuilder);
    }
    
}