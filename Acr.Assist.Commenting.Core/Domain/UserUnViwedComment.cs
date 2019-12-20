using System;

namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class UserUnViewedComment
    {
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the topic.
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        public Guid CommentID { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the created user image path.
        /// </summary>
        public string CreatedUserImagePath { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the created user identifier.
        /// </summary>
        public string CreatedUserID { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        public DateTime UpdatedDateTime { get; set; }
    }
}
