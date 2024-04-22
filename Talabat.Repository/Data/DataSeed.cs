using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Repository.Data
{
	public static class DataSeed
	{
        public static async Task SeedAsync(ApplicationDbContext _context)
        {
            if(!_context.ProductBrand.Any())
            {
                var brandJsonDataFile = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandJsonDataFile);
                if(brand?.Count() > 0)
                {
                   await _context.Set<ProductBrand>().AddRangeAsync(brand);
                   await _context.SaveChangesAsync();
                }
            }

			if (!_context.ProductCategories.Any())
			{
				var categoriesJsonDataFile = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesJsonDataFile);
				if (categories?.Count() > 0)
				{
					await _context.Set<ProductCategory>().AddRangeAsync(categories);
					await _context.SaveChangesAsync();
				}
			}


			if (!_context.Products.Any())
			{
				var productsJsonDataFile = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsJsonDataFile);
				if (products?.Count() > 0)
				{
					await _context.Set<Product>().AddRangeAsync(products);
					await _context.SaveChangesAsync();
				}
			}
		}
    }
}
