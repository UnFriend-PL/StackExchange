using System.Text.Json.Serialization;

namespace stackExchange.Models
{
    public class Tag
    {
        /// <summary>
        /// The name of the Stack Overflow tag.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The number of questions associated with the tag.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        /// <summary>
        /// Indicates whether the tag has synonyms.
        /// </summary>
        [JsonPropertyName("has_synonyms")]
        public bool HasSynonyms { get; set; } = false;

        /// <summary>
        /// Indicates whether the tag is for moderators only.
        /// </summary>
        [JsonPropertyName("is_moderator_only")]
        public bool IsModeratorOnly { get; set; } = false;

        /// <summary>
        /// Indicates whether the tag is required for a question.
        /// </summary>
        [JsonPropertyName("is_required")]
        public bool IsRequired { get; set; } = false;
    }
}
