using Newtonsoft.Json;

namespace stackExchange.Models.Tag
{
    public class TagResponse
    {
        [JsonProperty("items")]
        public List<TagDto> Items { get; set; } = new List<TagDto>();
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
    }
}
