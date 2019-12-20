using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.Commenting.Core.Profile
{
    public class CommentEntryProfiles : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentEntryProfiles"/> class.
        /// </summary>
        public CommentEntryProfiles()
        {
            CreateMap<AddCommentEntry, CommentEntry>();
        }
    }
}