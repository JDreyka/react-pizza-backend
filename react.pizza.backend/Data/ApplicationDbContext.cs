using Microsoft.EntityFrameworkCore;
using react.pizza.backend.Models;

namespace react.pizza.backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
    }
}