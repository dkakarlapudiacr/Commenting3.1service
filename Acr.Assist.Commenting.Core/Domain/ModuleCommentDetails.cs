using System;

namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class ModuleCommentDetails
    {
        /// <summary>
        /// Gets or sets the name of the topic.
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        public Guid CommentID { get; set; }
    }
}
