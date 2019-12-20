namespace Acr.Assist.CommentMicroService.Core.DTO
{
    public class UserCommentsViewEntry
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the topic identifier.
        /// </summary>
        public string TopicID { get; set; }
    }
}
