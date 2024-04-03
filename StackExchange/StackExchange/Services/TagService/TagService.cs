using Microsoft.EntityFrameworkCore;
using stackExchange.Database;
using stackExchange.Models.Tag;
using System.Globalization;

namespace stackExchange.Services.TagService
{
    public class TagService : ITagService
    {
       
        private readonly StackOverflowDbContext _context;
        private readonly ILogger<TagService> _logger;
        public TagService(StackOverflowDbContext stackOverflowDbContext, ILogger<TagService> logger) {
            _context = stackOverflowDbContext;
            _logger = logger;
        }

        public async Task<List<TagResult>> GetTagsAsync(int page, int pageSize, string? sortByName, string? descending)
        {
            List<TagResult> tags;
            try
            {
               
                IQueryable<Tag> query = _context.Tags;

                // sorting in descending (descending)
                if (!string.IsNullOrEmpty(descending) && descending.ToLower() == "true")
                {
                    query = query.OrderByDescending(t => t.Count);
                }

                // sorting in ascending (descending)
                if (!string.IsNullOrEmpty(descending) && descending.ToLower() == "false")
                {
                    query = query.OrderBy(t => t.Count);
                }

                // (sortByName)
                if (!string.IsNullOrEmpty(sortByName))
                {
                    switch (sortByName.ToLower())
                    {
                        case "asc":
                            query = query.OrderBy(t => t.Name);
                            break;
                        case "desc":
                            query = query.OrderByDescending(t => t.Name);
                            break;
                    }
                }

                tags = await query.Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Select(t => new TagResult(t))
                                 .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting tags");
                return null;
            }
            return tags;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            try
            {
                return await _context.Tags.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting tags");
                return null;
            }
        }
    }
}
