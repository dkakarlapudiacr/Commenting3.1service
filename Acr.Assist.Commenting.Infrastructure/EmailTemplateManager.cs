using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Email;
using System.Collections.Generic;

namespace Acr.Assist.Commenting.Infrastructure
{
    /// <summary>
    /// Business service for managing email related information
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Infrastructure.Email.IEmailTemplateManager" />
    public class EmailTemplateManager : IEmailTemplateManager
    {
        /// <summary>
        /// Gets the mentioned email.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="modulename">The modulename.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="addresses">The addresses.</param>
        /// <param name="applicationUrl">The Application Url.</param>
        /// <returns></returns>
        public EmailMessage GetMentionedEmail(string firstName, string modulename, string commentText, string applicationUrl,
                                              List<EmailAddressDetails> addresses)
        {
            var email = new EmailMessage();
            email.Type = EmailType.Support;
            email.FromAddress = new EmailAddressDetails("acr-assist@acr.org", "Marval Support");
            email.ToAddress = addresses;

            email.Subject = "Marval - User mentioned";
            email.Body = "Dear " + firstName + ",<br /><br />";
            email.Body += string.Format("You have been mentioned in the comment \'{0}\' for Module \"{1}\"", commentText, modulename);
            email.Body += "<br />To reply to this comment please login to <a href=" + applicationUrl + ">Marval</a>.";
            email.Body += "<br /><br /><b>Note: This is an auto generated email, please do not reply to this email.</b><br /><br />";
            email.Body += "Regards,<br />Marval Support";
            return email;
        }
    }
}
