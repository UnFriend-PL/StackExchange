using Microsoft.EntityFrameworkCore;
using stackExchange.Database;
using stackExchange.Models.Tag;

namespace stackExchange.Services.TagService
{
    public class TagService : ITagService
    {
       
        private readonly StackOverflowDbContext _context;
        public TagService(StackOverflowDbContext stackOverflowDbContext) {
            _context = stackOverflowDbContext;
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }
    }
}
