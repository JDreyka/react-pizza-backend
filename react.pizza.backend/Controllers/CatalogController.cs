using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using react.pizza.backend.Data;
using react.pizza.backend.Models;

namespace react.pizza.backend.Controllers
{
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;

        public CatalogController(IMapper mapper, ApplicationDbContext dbContext)
        {
            this.mapper = mapper;
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

        [HttpPost("item/create")]
        public async Task<IActionResult> CreateItem([FromBody] CatalogItemDto catalogItemDto)
        {
            var catalogItem = mapper.Map<CatalogItem>(catalogItemDto);

            var result = await dbContext.CatalogItems.AddAsync(catalogItem);
            await dbContext.SaveChangesAsync();

            return Ok(result.Entity.Id);
        }

        [HttpPatch("item/{id:int}/update")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] CatalogItemDto catalogItemDto)
        {
            var item = await dbContext.CatalogItems.FindAsync(id);
            if (item == null)
                return BadRequest($"Элемента с id:{id} не существует");
            var updatedItem = mapper.Map(catalogItemDto, item);

            dbContext.CatalogItems.Update(updatedItem);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("item/{id:int}/delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await dbContext.CatalogItems.FindAsync(id);
            if (item == null)
                return BadRequest($"Элемента с id:{id} не существует");

            var result = dbContext.CatalogItems.Remove(item);
            await dbContext.SaveChangesAsync();

            return Ok(result.Entity.Id);
        }
    }
}