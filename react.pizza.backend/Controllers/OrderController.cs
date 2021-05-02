using Microsoft.AspNetCore.Mvc;

namespace react.pizza.backend.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateOrder()
        {
            return Ok();
        }
    }
}