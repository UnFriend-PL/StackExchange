namespace stackExchange.Models.Tag
{
    public class TagResult : Tag
    {
        /// <summary>
        /// The number of times this tag was used on a post definied in %.
        /// </summary>
        public decimal distribution { get; set; }
    }
}
