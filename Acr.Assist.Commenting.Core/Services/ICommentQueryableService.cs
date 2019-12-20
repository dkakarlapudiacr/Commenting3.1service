using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
namespace Acr.Assist.CommentMicroService.Core.Services
{
    /// <summary>
    /// Interface for the comment querable related operations for a user
    /// </summary>
    public interface ICommentQueryableService
    {
        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<List<Comment>> GetComments(CommentsFilter filter);
    }
}
