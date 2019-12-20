using System;
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class Comment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
            CommentNotificationUsers = new List<CommentNotification>();
            CommentSuggestions = new List<CommentSuggestion>();
            CommentViews = new List<CommentView>();
            TotalSuggestedCount = 0;
        }

        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        /// <value>
        /// The comment identifier.
        /// </value>
        public Guid CommentID { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public string ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the module version.
        /// </summary>
        public string ModuleVersion { get; set; }

        /// <summary>
        /// Gets or sets the name of the topic.
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the created user identifier.
        /// </summary>
        public string CreatedUserID { get; set; }

        /// <summary>
        /// Gets or sets the created user image path.
        /// </summary>
        public string CreatedUserImagePath { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        public DateTime UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public CommentStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the proposed date time.
        /// </summary>
        public DateTime ProposedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the proposed user identifier.
        /// </summary>
        public string ProposedUserID { get; set; }

        /// <summary>
        /// Gets or sets the proposed user.
        /// </summary>
        public string ProposedUser { get; set; }

        /// <summary>
        /// Gets or sets the implemented date time.
        /// </summary>
        public DateTime ImplementedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the implemented user identifier.
        /// </summary>
        public string ImplementedUserID { get; set; }

        /// <summary>
        /// Gets or sets the implemented user.
        /// </summary>
        public string ImplementedUser { get; set; }

        /// <summary>
        /// Gets or sets the implemented comment.
        /// </summary>
        public string ImplementedComment { get; set; }

        /// <summary>
        /// Gets or sets the implemented module version.
        /// </summary>
        public string ImplementedModuleVersion { get; set; }

        /// <summary>
        /// Gets or sets the total suggested count.
        /// </summary>
        public int TotalSuggestedCount { get; set; }

        /// <summary>
        /// Gets or sets the comment notification users.
        /// </summary>
        public List<CommentNotification> CommentNotificationUsers { get; set; }

        /// <summary>
        /// Gets or sets the comment suggestions.
        /// </summary>
        public List<CommentSuggestion> CommentSuggestions { get; set; }

        /// <summary>
        /// Gets or sets the comment views.
        /// </summary>
        public List<CommentView> CommentViews { get; set; }
    }
}
