using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Domain;

namespace Acr.Assist.CommentMicroService.Core.Data
{
    /// <summary>
    /// Interface for the comment notification related repository operations
    /// </summary>
    public interface ICommentNotificationRepository
    {
        /// <summary>
        /// Adds the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="notificationUser">The notification user.</param>
        /// <returns></returns>
        Task AddCommentNotification(Guid commentID, CommentNotification notificationUser);

        /// <summary>
        /// Deletes the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task DeleteCommentNotification(Guid commentID, string userID);
    }
}
