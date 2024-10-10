using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContentSeed
{
    public static async Task SeedAsync(StoreContent context)
    {
        if(!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if(products == null) return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}