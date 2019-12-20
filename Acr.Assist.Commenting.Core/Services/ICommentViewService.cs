using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.CommentMicroService.Core.Services
{
    /// <summary>
    /// Interface for the comment view related operations for a user
    /// </summary>
    public interface ICommentViewService
    {
        /// <summary>
        /// Adds the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userEntry">The user entry.</param>
        /// <returns></returns>
        Task AddCommentView(Guid commentID, CommentUserEntry userEntry);

        /// <summary>
        /// Deletes the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task DeleteCommentView(Guid commentID, string userID);
    }
}
