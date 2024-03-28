using Microsoft.AspNetCore.Mvc;
using stackExchange.Services.TagService;
using System.Threading.Tasks;

namespace stackExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetTagsAsync()
        {
            var result = await _tagService.GetAllTags();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Tags")]
        public async Task<IActionResult> GetTagsAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 25,
            [FromQuery] string sortByName = null,
            [FromQuery] string descending = null)
        {
            var result = await _tagService.GetTagsAsync(page, pageSize, sortByName, descending);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
