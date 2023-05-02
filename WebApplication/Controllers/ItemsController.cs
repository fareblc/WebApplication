using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services.ItemService;

namespace WebApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("items"), Authorize]
        public async Task<IActionResult> GetItemsAsync()
        {
            var items = await _itemService.GetItemsAsync();
            return Ok(items);
        }
    }
}
