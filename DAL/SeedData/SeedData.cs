using Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace DAL.SeedData;

public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {

        // Set of Products
        modelBuilder.Entity<Product>().HasData(
            new Product() { Id = 1, Name = "Waistcoat", Price = 199 },
            new Product() { Id = 2, Name = "Socks", Price = 30 },
            new Product() { Id = 3, Name = "Blazer", Price = 149 },
            new Product() { Id = 4, Name = "Swimming Shorts", Price = 89 },
            new Product() { Id = 5, Name = "Suit", Price = 999 },
            new Product() { Id = 6, Name = "Jeans", Price = 299 },
            new Product() { Id = 7, Name = "Boots", Price = 499 },
            new Product() { Id = 8, Name = "Lingerie", Price = 149 },
            new Product() { Id = 9, Name = "Swimwear", Price = 700 },
            new Product() { Id = 10, Name = "Tracksuit", Price = 400 },
            new Product() { Id = 11, Name = "Shawl", Price = 99 },
            new Product() { Id = 12, Name = "Stockings", Price = 50 },
            new Product() { Id = 13, Name = "Shirt", Price = 400 },
            new Product() { Id = 14, Name = "Briefs", Price = 129 },
            new Product() { Id = 15, Name = "Scarf", Price = 59 },
            new Product() { Id = 16, Name = "Blouse", Price = 129 },
            new Product() { Id = 17, Name = "Gloves", Price = 79 },
            new Product() { Id = 18, Name = "Polo Shirt", Price = 299 },
            new Product() { Id = 19, Name = "Belt", Price = 69 },
            new Product() { Id = 20, Name = "Cravat", Price = 69 }
            );

        // Set of ProductCharacteristic
        modelBuilder.Entity<ProductCharacteristic>().HasData(
            new ProductCharacteristic() { ProductId = 1, Name = "Material", Value = "Wool" },
            new ProductCharacteristic() { ProductId = 2, Name = "Brand", Value = "ChinaTown" },
            new ProductCharacteristic() { ProductId = 3, Name = "Material", Value = "Polyester" },
            new ProductCharacteristic() { ProductId = 4, Name = "Material", Value = "Nylon" },
            new ProductCharacteristic() { ProductId = 5, Name = "Material", Value = "Cotton" },
            new ProductCharacteristic() { ProductId = 6, Name = "Material", Value = "Denim" },
            new ProductCharacteristic() { ProductId = 7, Name = "Material", Value = "Leather" },
            new ProductCharacteristic() { ProductId = 8, Name = "Brand", Value = "Victoria`s Secret" },
            new ProductCharacteristic() { ProductId = 9, Name = "Material", Value = "Nylon" },
            new ProductCharacteristic() { ProductId = 10, Name = "Material", Value = "Nylon" },
            new ProductCharacteristic() { ProductId = 11, Name = "Brand", Value = "Guess" },
            new ProductCharacteristic() { ProductId = 12, Name = "Brand", Value = "Nike" },
            new ProductCharacteristic() { ProductId = 13, Name = "Brand", Value = "Tom Ford" },
            new ProductCharacteristic() { ProductId = 14, Name = "Brand", Value = "Tom Ford" },
            new ProductCharacteristic() { ProductId = 15, Name = "Brand", Value = "Guess" },
            new ProductCharacteristic() { ProductId = 16, Name = "Material", Value = "Cotton" },
            new ProductCharacteristic() { ProductId = 17, Name = "Brand", Value = "Green Hill" },
            new ProductCharacteristic() { ProductId = 18, Name = "Brand", Value = "Adidas" },
            new ProductCharacteristic() { ProductId = 19, Name = "Brand", Value = "Victoria`s Secret" },
            new ProductCharacteristic() { ProductId = 20, Name = "Brand", Value = "ChinaTown" }
            );

        // Set of StorageItems
        modelBuilder.Entity<StorageItem>().HasData(
            new StorageItem() { ProductId = 1, Amount = 35 },
            new StorageItem() { ProductId = 2, Amount = 150 },
            new StorageItem() { ProductId = 3, Amount = 50 },
            new StorageItem() { ProductId = 4, Amount = 47 },
            new StorageItem() { ProductId = 5, Amount = 13 },
            new StorageItem() { ProductId = 6, Amount = 259 },
            new StorageItem() { ProductId = 7, Amount = 67 },
            new StorageItem() { ProductId = 8, Amount = 50 },
            new StorageItem() { ProductId = 9, Amount = 45 },
            new StorageItem() { ProductId = 10, Amount = 156 },
            new StorageItem() { ProductId = 11, Amount = 69 },
            new StorageItem() { ProductId = 12, Amount = 645 },
            new StorageItem() { ProductId = 13, Amount = 76 },
            new StorageItem() { ProductId = 14, Amount = 45 },
            new StorageItem() { ProductId = 15, Amount = 213 },
            new StorageItem() { ProductId = 16, Amount = 56 },
            new StorageItem() { ProductId = 17, Amount = 75 },
            new StorageItem() { ProductId = 18, Amount = 56 },
            new StorageItem() { ProductId = 19, Amount = 34 },
            new StorageItem() { ProductId = 20, Amount = 43 }
            );

        // Set of Storage will be automatically created

        // Set of Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer() { Login = "heidrich@me.com", Name = "Adelyn", Surname = "Sawyer", Password = "9*TeG*vmg1%PKnOb", BirthDay = new DateOnly(2000, 11, 13) },
            new Customer() { Login = "mcrawfor@mac.com", Name = "Ernest", Surname = "Duke", Password = "Dm8-k&Qov@*N+9pw", BirthDay = new DateOnly(2003, 9, 25) },
            new Customer() { Login = "attwood@optonline.net", Name = "Nathen", Surname = "Becker", Password = "J8=WHk7IvVe1IWfZ", BirthDay = new DateOnly(2005, 7, 2) },
            new Customer() { Login = "aegreene@optonline.net", Name = "Keyon", Surname = "Snyder", Password = "AQnNNu?43i557vdV", BirthDay = new DateOnly(1984, 8, 17) },
            new Customer() { Login = "ralamosm@comcast.net", Name = "Kyan", Surname = "Bray", Password = "90OWn8pd=2c5ba_m", BirthDay = new DateOnly(1984, 7, 12) },
            new Customer() { Login = "miami@aol.com", Name = "Enzo", Surname = "Costa", Password = "1HwyNX^D5lt1&xe3", BirthDay = new DateOnly(1992, 4, 2) },
            new Customer() { Login = "aegreene@me.com", Name = "Janae", Surname = "Brennan", Password = "dKtG3ZgF", BirthDay = new DateOnly(1983, 10, 31) },
            new Customer() { Login = "nasor@aol.com", Name = "Branson", Surname = "Vazquez", Password = "4+729dx8", BirthDay = new DateOnly(1987, 5, 25) },
            new Customer() { Login = "epeeist@att.net", Name = "Jairo", Surname = "Graves", Password = "rfB5vO6k", BirthDay = new DateOnly(1999, 11, 2) },
            new Customer() { Login = "jdray@icloud.com", Name = "Paula", Surname = "Giles", Password = "0ONy2LUn", BirthDay = new DateOnly(1992, 6, 22) }
            );

        // Set of PaymentConfig
        modelBuilder.Entity<PaymentConfig>().HasData(
            new PaymentConfig() { CustomerId = 1, Type = "MasterCard" },
            new PaymentConfig() { CustomerId = 2, Type = "MasterCard" },
            new PaymentConfig() { CustomerId = 3, Type = "MasterCard" },
            new PaymentConfig() { CustomerId = 4, Type = "MasterCard" },
            new PaymentConfig() { CustomerId = 5, Type = "MasterCard" },
            new PaymentConfig() { CustomerId = 6, Type = "Visa" },
            new PaymentConfig() { CustomerId = 7, Type = "Visa" },
            new PaymentConfig() { CustomerId = 8, Type = "Visa" },
            new PaymentConfig() { CustomerId = 9, Type = "Visa" },
            new PaymentConfig() { CustomerId = 10, Type = "Visa" }
            );

        // Set of Orders
        modelBuilder.Entity<Order>().HasData(
            new Order() { CustomerId = 1, Id = 1, OrderDate= new DateTime(2022, 10, 13) },
            new Order() { CustomerId = 2, Id = 2, OrderDate = new DateTime(2022, 10, 13) },
            new Order() { CustomerId = 3, Id = 3, OrderDate = new DateTime(2022, 10, 14) },
            new Order() { CustomerId = 4, Id = 4, OrderDate = new DateTime(2022, 10, 15) },
            new Order() { CustomerId = 5, Id = 5, OrderDate = new DateTime(2022, 10, 20) },
            new Order() { CustomerId = 6, Id = 6, OrderDate = new DateTime(2022, 11, 13) },
            new Order() { CustomerId = 7, Id = 7, OrderDate = new DateTime(2022, 11, 30) },
            new Order() { CustomerId = 8, Id = 8, OrderDate = new DateTime(2022, 12, 22) },
            new Order() { CustomerId = 9, Id = 9, OrderDate = new DateTime(2022, 12, 14) },
            new Order() { CustomerId = 10, Id = 10, OrderDate = new DateTime(2022, 12, 13) }
            );

        // Set of Checks
        modelBuilder.Entity<Check>().HasData(
            new Check() { OrderId = 1 },
            new Check() { OrderId = 2 },
            new Check() { OrderId = 3 },
            new Check() { OrderId = 4 },
            new Check() { OrderId = 5 },
            new Check() { OrderId = 6 },
            new Check() { OrderId = 7 },
            new Check() { OrderId = 8 },
            new Check() { OrderId = 9 },
            new Check() { OrderId = 10 }
            );

        // Set of Carts
        modelBuilder.Entity<Cart>().HasData(
            new Cart() { CustomerId = 1 },
            new Cart() { CustomerId = 2 },
            new Cart() { CustomerId = 3 },
            new Cart() { CustomerId = 4 },
            new Cart() { CustomerId = 5 },
            new Cart() { CustomerId = 6 },
            new Cart() { CustomerId = 7 },
            new Cart() { CustomerId = 8 },
            new Cart() { CustomerId = 9 },
            new Cart() { CustomerId = 10 }
            ); 
    }
}