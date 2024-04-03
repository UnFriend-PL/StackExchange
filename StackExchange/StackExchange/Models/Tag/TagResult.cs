using stackExchange.Common.TagStatistics;

namespace stackExchange.Models.Tag
{
    public class TagResult : Tag
    {
        /// <summary>
        /// The number of times this tag was used on a post definied in %.
        /// </summary>
        public decimal Distribution { get; set; }

        public TagResult(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
            Count = tag.Count;
            HasSynonyms = tag.HasSynonyms;
            IsModeratorOnly = tag.IsModeratorOnly;
            IsRequired = tag.IsRequired;
            Distribution = Math.Round(tag.Count / (decimal)TagStatistics.TotalCountOfTags, 3);
        }
        public TagResult() { }
    }
}
