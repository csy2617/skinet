using System;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Config;

namespace Infrastructure.Data;

public class StoreContent(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
