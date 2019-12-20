using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.CommentMicroService.Core.Data
{
    /// <summary>
    /// Interface for the comment related repository operations
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Checks if comment exist.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <returns></returns>
        Task<bool> CheckIfCommentExist(Guid commentID);

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task<Guid> AddComment(CommentEntry comment);

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <returns></returns>
        Task DeleteComment(Guid commentID);

        /// <summary>
        /// Edits the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        Task EditComment(CommentUpdateResult comment);

        /// <summary>
        /// Proposes the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userEntry">The user entry.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        Task ProposeComment(Guid commentID, CommentUserEntry userEntry, DateTime dateTime);

        /// <summary>
        /// Implements the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentImplementEntry">The comment implement entry.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        Task ImplementComment(Guid commentID, CommentImplementEntry commentImplementEntry, DateTime dateTime);

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
