using System;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.CommentMicroService.Core.Services
{
    /// <summary>
    /// Interface for the comment suggestion related operations for a user
    /// </summary>
    public interface ICommentSuggestionService
    {
        /// <summary>
        /// Adds the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<CommentSuggestion> AddCommentSuggestion(Guid commentID, CommentUserEntry user);

        /// <summary>
        /// Deletes the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task DeleteCommentSuggestion(Guid commentID, string userID);        
    }
}
