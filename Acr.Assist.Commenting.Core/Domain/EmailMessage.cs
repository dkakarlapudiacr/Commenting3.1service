
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Core.Domain
{
    public class EmailMessage
    {
        /// <summary>
        /// Gets or sets the type of the email.
        /// </summary>
        public EmailType Type { get; set; }

        /// <summary>
        /// Gets or sets from address.
        /// </summary>
        public EmailAddressDetails FromAddress { get; set; }

        /// <summary>
        /// Gets or sets to address.
        /// </summary>
        public List<EmailAddressDetails> ToAddress { get; set; }

        /// <summary>
        /// Gets or sets the cc address.
        /// </summary>
        public List<EmailAddressDetails> CcAddress { get; set; }

        /// <summary>
        /// Gets or sets the BCC address.
        /// </summary>
        public List<EmailAddressDetails> BccAddress { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the email attachment.
        /// </summary>
        public List<EmailAttachment> Attachments { get; set; }
    }
}