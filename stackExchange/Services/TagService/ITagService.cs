using stackExchange.Models.Tag;

namespace stackExchange.Services.TagService
{
    public interface ITagService
    {
        Task<List<Tag>> GetTagsAsync();
    }
}
