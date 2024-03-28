using Newtonsoft.Json;

namespace stackExchange.Models.Tag
{
    public class TagResponse
    {
        [JsonProperty("items")]
        public List<TagDto> Items { get; set; }
    }
}
