using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Core.Services
{    
    /// <summary>
    /// Interface for the commenting related operations for a user
    /// </summary>
    public interface IUserCommentService
    {
        /// <summary>
        /// Gets the un viwed comment.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<List<UserUnViewedComment>> GetUnViwedComment(string userID);

        /// <summary>
        /// Updates the comments as viewed.
        /// </summary>
        /// <param name="userCommentsView">The user comments view.</param>
        /// <returns></returns>
        Task UpdateCommentsAsViewed(UserCommentsViewEntry userCommentsView);
    }
}
