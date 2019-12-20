using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Domain;
namespace Acr.Assist.CommentMicroService.Core.Data
{
    /// <summary>
    /// Interface for the comment suggestions related repository operations
    /// </summary>
    public interface ICommentSuggestionRepository
    {
        /// <summary>
        /// Adds the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentSuggestion">The comment suggestion.</param>
        /// <returns></returns>
        Task AddCommentSuggestion(Guid commentID, CommentSuggestion commentSuggestion);

        /// <summary>
        /// Deletes the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task DeleteCommentSuggestion(Guid commentID, string userID);       
    }
}
