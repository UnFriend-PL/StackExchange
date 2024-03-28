using stackExchange.Models.Tag;

namespace stackExchange.Services.TagService
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTags();
        Task<List<TagResult>> GetTagsAsync(int page, int pageSize, string sortBy, string descending);
    }
}
