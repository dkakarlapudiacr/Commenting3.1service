using Acr.Assist.CommentMicroService.Core.Domain;
using System.Collections.Generic;

namespace Acr.Assist.CommentMicroService.Core.Infrastructure.Email
{
    /// <summary>
    /// Interface for managing email related information
    /// </summary>
    public interface IEmailTemplateManager
    {
        /// <summary>
        /// Gets the user request for admin.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="modulename">The modulename.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="applicationUrl">The Application Url.</param>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        EmailMessage GetMentionedEmail(string firstName, string modulename, string commentText, string applicationUrl, List<EmailAddressDetails> addresses);
    }
}