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

        [HttpGet]
        public async Task<IActionResult> GetTagsAsync()
        {
            var result = await _tagService.GetTagsAsync();
            return Ok(result);
        }
    }
}
