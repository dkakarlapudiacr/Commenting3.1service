using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Service.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service
{
    /// <summary>
    /// Class for the comment querable related operations for a user
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.ICommentQueryableService" />
    public class CommentQueryableService : ICommentQueryableService
    {
        /// <summary>
        /// The comment queryable repository
        /// </summary>
        private readonly ICommentQueryableRepository commentQueryableRepository;

        /// <summary>
        /// The comments filter validator
        /// </summary>
        private readonly IDataValidator<CommentsFilter> commentsFilterValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentQueryableService"/> class.
        /// </summary>
        /// <param name="commentQueryableRepository">The comment queryable repository.</param>
        /// <param name="commentsFilterValidator">The comments filter validator.</param>
        public CommentQueryableService(
            ICommentQueryableRepository commentQueryableRepository, 
            IDataValidator<CommentsFilter> commentsFilterValidator)
        {
            this.commentQueryableRepository = commentQueryableRepository;
            this.commentsFilterValidator = commentsFilterValidator;
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<List<Comment>> GetComments(CommentsFilter filter)
        {
            var validatorResult = commentsFilterValidator.ValidateInstance(filter);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }            
            return await commentQueryableRepository.GetComments(filter);
        }
    }
}
