using stackExchange.Models.Tag;

namespace stackExchange.Services.TagService
{
    public interface ITagDownloaderService
    {
        public Task<int> UpdateTagsAsync(bool? force = false);
    }
}
