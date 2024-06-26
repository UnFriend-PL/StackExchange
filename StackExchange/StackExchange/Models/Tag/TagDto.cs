﻿using Newtonsoft.Json;

namespace stackExchange.Models.Tag
{
    public class TagDto
    {
        /// <summary>
        /// The name of the Stack Overflow tag.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The number of questions associated with the tag.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; } = 0;

        /// <summary>
        /// Indicates whether the tag has synonyms.
        /// </summary>
        [JsonProperty("has_synonyms")]
        public bool HasSynonyms { get; set; } = false;

        /// <summary>
        /// Indicates whether the tag is for moderators only.
        /// </summary>
        [JsonProperty("is_moderator_only")]
        public bool IsModeratorOnly { get; set; } = false;

        /// <summary>
        /// Indicates whether the tag is required for a question.
        /// </summary>
        [JsonProperty("is_required")]
        public bool IsRequired { get; set; } = false;
    }
}
