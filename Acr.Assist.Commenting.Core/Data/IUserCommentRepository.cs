using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Core.Data
{
    /// <summary>
    /// Interface for the user comment related repository operations
    /// </summary>
    public interface IUserCommentRepository
    {
        /// <summary>
        /// Gets the unviwed comment.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<List<UserUnViewedComment>> GetUnViwedComment(string userID);

        /// <summary>
        /// Updates the comments as viewed.
        /// </summary>
        /// <param name="userCommentsView">The user comments view.</param>
        /// <param name="commentView">The comment view.</param>
        /// <returns></returns>
        Task UpdateCommentsAsViewed(UserCommentsViewEntry userCommentsView, CommentView commentView);
    }
}
