using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.Commenting.Core.Profile
{
    public class CommentImplementationProfiles : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentImplementationProfiles"/> class.
        /// </summary>
        public CommentImplementationProfiles()
        {
            CreateMap<CommentImplementEntry, CommentImplementResult>();
        }
    }
}