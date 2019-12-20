using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.CommentMicroService.Core.Services
{
    /// <summary>
    /// Interface for the commenting related operations
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="commentEntry">The comment entry.</param>
        /// <returns></returns>
        Task<CommentEntry> AddComment(AddCommentEntry commentEntry);

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentIDDetails">The comment identifier details.</param>
        /// <returns></returns>
        Task DeleteComment(CommentIDDetails commentIDDetails);

        /// <summary>
        /// Edits the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentEntry">The comment entry.</param>
        /// <returns></returns>
        Task<CommentUpdateResult> EditComment(Guid commentID, UpdateCommentEntry commentEntry);

        /// <summary>
        /// Proposes the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userEntry">The user entry.</param>
        /// <returns></returns>
        Task ProposeComment(Guid commentID, CommentUserEntry userEntry);

        /// <summary>
        /// Implements the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentImplementEntry">The comment implement entry.</param>
        /// <returns></returns>
        Task<CommentImplementResult> ImplementComment(Guid commentID, CommentImplementEntry commentImplementEntry);

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<List<CommentEntry>> GetComments(CommentsFilter filter);

        /// <summary>
        /// Deletes the comment based on module id and topic id
        /// </summary>
        /// <param name="deleteComment"></param>
        /// <returns></returns>
        Task DeleteTopicComments(DeleteComment deleteComment);
    }
}
