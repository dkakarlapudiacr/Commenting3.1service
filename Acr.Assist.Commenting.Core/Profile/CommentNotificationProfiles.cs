using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.Commenting.Core.Profile
{
    public class CommentNotificationProfiles : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentNotificationProfiles"/> class.
        /// </summary>
        public CommentNotificationProfiles()
        {
            CreateMap<CommentNotificationEntry, CommentNotification>();
        }
    }
}