namespace Acr.Assist.CommentMicroService.Core.DTO
{
    public class CommentsFilter
    {
        /// <summary>
        /// Gets or sets the topic identifier.
        /// </summary>
        public string TopicID { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the skip count.
        /// </summary>
        public int SkipCount { get; set; }

        /// <summary>
        /// Gets or sets the take count.
        /// </summary>
        public int TakeCount { get; set; }
    }
}
