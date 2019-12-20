using Acr.Assist.CommentMicroService.Core.Domain;
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Core.DTO
{
    public class UpdateCommentEntry
    {
        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the Application Url.
        /// </summary>
        public string ApplicationUrl { get; set; }

        /// <summary>
        /// Gets or sets the email message.
        /// </summary>
        public List<EmailAddressDetails> ToRecipients { get; set; }
    }
}
