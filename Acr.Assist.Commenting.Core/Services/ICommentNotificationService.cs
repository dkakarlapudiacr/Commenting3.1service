using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.CommentMicroService.Core.Services
{
    /// <summary>
    /// Interface for the comment noticaition related operations for a user
    /// </summary>
    public interface ICommentNotificationService
    {
        /// <summary>
        /// Adds the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="notificationEntry">The notification entry.</param>
        /// <returns></returns>
        Task AddCommentNotification(Guid commentID, CommentNotificationEntry notificationEntry);

        /// <summary>
        /// Deletes the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task DeleteCommentNotification(Guid commentID, string userID);
    }
}
