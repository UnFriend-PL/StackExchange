using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using stackExchange.Common.TagStatistics;
using stackExchange.Database;
using stackExchange.Models.Tag;
using System.IO.Compression;
using System.Numerics;

namespace stackExchange.Services.TagService
{
    public class TagDownloaderService : ITagDownloaderService
    {
        private readonly HttpClient _httpClient;
        private readonly StackOverflowDbContext _context;
        private readonly ILogger<TagDownloaderService> _logger;

        private const int PageSize = 100; // Number of tags per API request

        public TagDownloaderService(HttpClient httpClient, StackOverflowDbContext context, ILogger<TagDownloaderService> logger)
        {
            _httpClient = httpClient;
            _context = context;
            _logger = logger;

        }

        public async Task<int> UpdateTagsAsync()
        {
            int totalTagsDownloaded = 0;
            int pageNumber = 1;
            int totalNewAddedTags = 0;
            int tagsInDB = await _context.Tags.CountAsync();

            if(tagsInDB >= 1000)
            {
                _logger.LogInformation("Tags already exist in the database. Skipping download.");
                await CalculateTotalCountOfTags();
                return 0;
            }
            _logger.LogInformation("Updating tags...");

            List<TagDto> tags = new List<TagDto>();

            do
            {
                var url = $"https://api.stackexchange.com/2.3/tags?order=desc&sort=popular&filter=default&site=stackoverflow&pagesize={PageSize}&page={pageNumber}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();

                    using (var decompressionStream = new GZipStream(contentStream, CompressionMode.Decompress))
                    using (var streamReader = new StreamReader(decompressionStream))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        var tagResponse = serializer.Deserialize<TagResponse>(jsonReader);

                        if (tagResponse?.Items != null && tagResponse.Items.Count > 0)
                        {
                            totalNewAddedTags += await SaveTagsToDbAsync(tagResponse.Items);
                            totalTagsDownloaded += tagResponse.Items.Count;
                        }
                    }

                    pageNumber++;
                }
                else
                {
                    _logger.LogError($"Error fetching tags from page {pageNumber}: {response.StatusCode}");
                    break;
                }
            } while (totalTagsDownloaded < 1000);
            _logger.LogDebug("Tags downloaded: {TotalTagsDownloaded}", totalTagsDownloaded);
            await CalculateTotalCountOfTags();
            return totalTagsDownloaded;
        }

        private async Task<int> SaveTagsToDbAsync(List<TagDto> tags)
        {
            var existingTagDtos = await _context.Tags.Select(t => new TagDto { Name = t.Name }).ToListAsync();
            var newTags = tags.Except(existingTagDtos, new TagComparer());
            var newTagEntities = newTags.Select(tagDto => new Tag { Name = tagDto.Name, Count = tagDto.Count, HasSynonyms = tagDto.HasSynonyms, IsModeratorOnly = tagDto.IsModeratorOnly, IsRequired = tagDto.IsRequired }).ToList();
            await _context.Tags.AddRangeAsync(newTagEntities);
            await _context.SaveChangesAsync();
            return newTagEntities.Count;
        }

        private async Task CalculateTotalCountOfTags()
        {
            try
            {
                _logger.LogInformation("Calculating total count of tags...");
                TagStatistics.TotalCountOfTags = await _context.Tags.Select(t => t.Count).SumAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating total count of tags");
            }
            finally
            {
            _logger.LogInformation("Total count of tags: {TotalCountOfTags}", TagStatistics.TotalCountOfTags);
            }
        }

    }

    public class TagComparer : IEqualityComparer<TagDto>
    {
        public bool Equals(TagDto x, TagDto y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(TagDto obj)
        {
            return obj.Name?.GetHashCode() ?? 0;
        }
    }

}
