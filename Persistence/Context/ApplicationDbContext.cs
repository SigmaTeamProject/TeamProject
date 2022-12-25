using Microsoft.EntityFrameworkCore;
using Persistence.SeedData;

namespace Persistence.Context;

public class ApplicationDbContext : DbContext
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ��� ��������

        SeedData.SeedData.Seed(modelBuilder);
    }
    
}