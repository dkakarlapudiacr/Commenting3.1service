using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;

namespace Acr.Assist.CommentMicroService.Core.Data
{
    /// <summary>
    /// Interface for the comment querable related repository operations
    /// </summary>
    public interface ICommentQueryableRepository
    {
        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<List<Comment>> GetComments(CommentsFilter filter);
    }
}
