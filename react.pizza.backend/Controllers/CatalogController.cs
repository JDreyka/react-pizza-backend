using System.Linq;
using Microsoft.AspNetCore.Mvc;
using react.pizza.backend.Data;

namespace react.pizza.backend.Controllers
{
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CatalogController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("items")]
        public IActionResult GetItems(int take = 20, int skip = 0)
        {
            var items = dbContext.CatalogItems
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take);
            return Ok(items);
        }
    }
}