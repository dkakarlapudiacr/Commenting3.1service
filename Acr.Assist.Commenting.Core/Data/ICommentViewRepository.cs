using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Domain;

namespace Acr.Assist.CommentMicroService.Core.Data
{
    /// <summary>
    /// Interface for the comment view related repository operations
    /// </summary>
    public interface ICommentViewRepository
    {
        /// <summary>
        /// Adds the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentView">The comment view.</param>
        /// <returns></returns>
        Task AddCommentView(Guid commentID, CommentView commentView);

        /// <summary>
        /// Deletes the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task DeleteCommentView(Guid commentID, string userID);
    }
}
