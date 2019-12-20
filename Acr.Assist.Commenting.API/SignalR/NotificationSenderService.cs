using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.SignalR
{
    /// <summary>
    /// Real time notification handled by this service
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.INotificationSenderService" />
    public class NotificationSenderService : INotificationSenderService
    {
        /// <summary>
        /// The hub context
        /// </summary>
        private readonly IHubContext<NotificationsHub> hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSenderService"/> class.
        /// </summary>
        /// <param name="hubContext">The hub context.</param>
        public NotificationSenderService(IHubContext<NotificationsHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        /// <summary>
        /// Notifies as comment added.
        /// </summary>
        /// <param name="userUnViewedComment">The user un viewed comment.</param>
        public async Task NotifyAsCommentAdded(UserUnViewedComment userUnViewedComment)
        {
            await hubContext.Clients.GroupExcept(userUnViewedComment.ModuleID.ToLower(), NotificationsHub.GetConnections(userUnViewedComment.CreatedUserID)).SendAsync("receive", userUnViewedComment);
        }
    }
}
