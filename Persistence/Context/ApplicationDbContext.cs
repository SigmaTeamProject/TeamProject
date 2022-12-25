using Microsoft.EntityFrameworkCore;
using Persistence.SeedData;

namespace Persistence.Context;

public class ApplicationDbContext : DbContext
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Код Любомира

        SeedData.SeedData.Seed(modelBuilder);
    }
    
}