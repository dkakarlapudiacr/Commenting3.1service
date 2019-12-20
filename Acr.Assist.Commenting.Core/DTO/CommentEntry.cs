using Acr.Assist.CommentMicroService.Core.Domain;
using System;
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Core.DTO
{
    public class CommentEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentEntry"/> class.
        /// </summary>
        public CommentEntry() { CommentSuggestions = new List<CommentSuggestion>(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentEntry"/> class.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentText">The comment text.</param>
        public CommentEntry(Guid commentID, string commentText)
        {
            CommentID = commentID;
            CommentText = commentText;
            CommentSuggestions = new List<CommentSuggestion>();
        }

        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        public Guid CommentID { get; set; }

        /// <summary>
        /// Gets or sets the created user image path.
        /// </summary>
        public string CreatedUserImagePath { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the name of the topic.
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        public string ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the created user identifier.
        /// </summary>
        public string CreatedUserID { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public CommentStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        public DateTime UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the module version.
        /// </summary>
        public string ModuleVersion { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

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
        /// Gets or sets the comment suggestions.
        /// </summary>
        public List<CommentSuggestion> CommentSuggestions { get; set; }
    }
}
