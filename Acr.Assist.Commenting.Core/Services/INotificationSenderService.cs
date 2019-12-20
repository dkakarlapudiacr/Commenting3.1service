using Acr.Assist.CommentMicroService.Core.Domain;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Core.Services
{
    /// <summary>
    /// Interface for the comment notification related operations
    /// </summary>
    public interface INotificationSenderService
    {
        /// <summary>
        /// Notifies as comment added.
        /// </summary>
        /// <param name="userUnViewedComment">The user un viewed comment.</param>
        /// <returns></returns>
        Task NotifyAsCommentAdded(UserUnViewedComment userUnViewedComment);
    }
}
