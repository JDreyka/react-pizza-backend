using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using react.pizza.backend.Data;
using react.pizza.backend.Models;

namespace react.pizza.backend.Services
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.CatalogItems.Any())
                return;

            if (!File.Exists("init-db-data.json"))
                return;

            var data = File.ReadAllText("init-db-data.json");

            var catalogItems = JsonSerializer.Deserialize<CatalogItem[]>(data) ?? Array.Empty<CatalogItem>();

            dbContext.CatalogItems.AddRange(catalogItems);

            dbContext.SaveChanges();
        }
    }
}