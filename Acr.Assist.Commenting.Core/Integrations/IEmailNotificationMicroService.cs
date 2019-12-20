using Acr.Assist.CommentMicroService.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Core.Integrations
{
    /// <summary>
    /// Interface for the email notifcation microservice
    /// </summary>
    public interface IEmailNotificationMicroService
    {
        /// <summary>
        /// Checks if user is rejected.
        /// </summary>
        /// <param name="emailMessage">The email message.</param>
        /// <returns></returns>
        Task<Guid> SendEmailNotification(EmailMessage emailMessage);
    }
}
