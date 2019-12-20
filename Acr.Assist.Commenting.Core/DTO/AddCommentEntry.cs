using Acr.Assist.CommentMicroService.Core.Domain;
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Core.DTO
{
    public class AddCommentEntry
    {
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
        /// Gets or sets the module version.
        /// </summary>
        public string ModuleVersion { get; set; }

        /// <summary>
        /// Gets or sets the Application Url.
        /// </summary>
        public string ApplicationUrl { get; set; }

        /// <summary>
        /// Gets or sets to recipients.
        /// </summary>
        public List<EmailAddressDetails> ToRecipients { get; set; }
    }
}
