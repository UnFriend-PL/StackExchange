using Microsoft.AspNetCore.Mvc;
using stackExchange.Services.TagService;
using System.Threading.Tasks;

/// <summary>
/// This controller handles all operations related to Tags.
/// </summary>
namespace stackExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ITagDownloaderService _tagDownloaderService;

        public TagController(ITagService tagService, ITagDownloaderService tagDownloaderService)
        {
            _tagService = tagService;
            _tagDownloaderService = tagDownloaderService;
        }

        /// <summary>
        /// Retrieves all tags asynchronously.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Retrieves a list of tags asynchronously.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of tags per page.</param>
        /// <param name="sortByName">The field to sort the tags by name. (asc, desc, empty)</param>
        /// <param name="descending">Specifies whether to sort the tags in descending order. (true, false, empty)</param>
        /// <returns>An <see cref="IActionResult"/> representing the asynchronous operation's result.</returns>
        [HttpGet("Tags")]
        public async Task<IActionResult> GetTagsAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 25,
            [FromQuery] string? sortByName = null,
            [FromQuery] string? descending = null)
        {
            var result = await _tagService.GetTagsAsync(page, pageSize, sortByName, descending);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Forces a download of tags asynchronously.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the asynchronous operation's result.</returns>
        [HttpPost("ForceDownload")]
        public async Task<IActionResult> ForceDownloadAsync()
        {
            var downloaded = await _tagDownloaderService.UpdateTagsAsync(true);
            if (downloaded == 0)
            {
                return BadRequest($"Downloaded {downloaded} tags. Error with fetching...");
            }
            return Ok($"Downloaded {downloaded} tags.");
        }
    }
}

