using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace stackExchange.Models.Tag
{
    public class Tag
    {
        /// <summary>
        /// The unique identifier for the tag.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of questions associated with the tag.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Indicates whether the tag has synonyms.
        /// </summary>
        public bool HasSynonyms { get; set; } = false;

        /// <summary>
        /// Indicates whether the tag is for moderators only.
        /// </summary>
        public bool IsModeratorOnly { get; set; } = false;

        /// <summary>
        /// Indicates whether the tag is required for a question.
        /// </summary>
        public bool IsRequired { get; set; } = false;
    }
}
