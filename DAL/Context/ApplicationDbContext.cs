using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DAL.Helpers;
using System.Diagnostics.Metrics;

namespace DAL.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Check> Checks { get; set; }
    public DbSet<StorageItem> StorageItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<PaymentConfig> PaymentConfigs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        //var connectionString = "Host=localhost;Username=aloshaprokopenko5;Password=787898;Database=sigma_db";
        optionsBuilder.UseSqlServer(connectionString).EnableSensitiveDataLogging();
        //optionsBuilder.UseNpgsql(connectionString).EnableSensitiveDataLogging();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CartConfigurations());
        modelBuilder.ApplyConfiguration(new CheckConfigurations());
        modelBuilder.ApplyConfiguration(new CustomerConfigurations());
        modelBuilder.ApplyConfiguration(new OrderConfigurations());
        modelBuilder.ApplyConfiguration(new PaymentConfigConfigurations());
        modelBuilder.ApplyConfiguration(new ProductCharacteristicConfigurations());
        modelBuilder.ApplyConfiguration(new StorageItemConfigurations());

        SeedData.SeedData.Seed(modelBuilder);
    }

}