using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using Acr.Assist.CommentMicroService.Service.Validator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service
{
    /// <summary>
    /// Class for the commenting related operations for a user
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.IUserCommentService" />
    public class UserCommentService : IUserCommentService
    {
        /// <summary>
        /// The user comment repository
        /// </summary>
        private readonly IUserCommentRepository userCommentRepository;

        /// <summary>
        /// The user comments validator
        /// </summary>
        private readonly IDataValidator<UserCommentsViewEntry> userCommentsValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommentService"/> class.
        /// </summary>
        /// <param name="userCommentRepository">The user comment repository.</param>
        /// <param name="userCommentsValidator">The user comments validator.</param>
        public UserCommentService(
            IUserCommentRepository userCommentRepository, 
            IDataValidator<UserCommentsViewEntry> userCommentsValidator)
        {
            this.userCommentRepository = userCommentRepository;
            this.userCommentsValidator = userCommentsValidator;
        }

        /// <summary>
        /// Gets the un viwed comment.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<List<UserUnViewedComment>> GetUnViwedComment(string userID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrWhiteSpace(userID))
            {
                throw new InputValidationFailureException(ExceptionMessages.UserIDEmpty);
            }

            return await userCommentRepository.GetUnViwedComment(userID);
        }

        /// <summary>
        /// Updates the comments as viewed.
        /// </summary>
        /// <param name="userCommentsView">The user comments view.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task UpdateCommentsAsViewed(UserCommentsViewEntry userCommentsView)
        {

            var validatorResult = userCommentsValidator.ValidateInstance(userCommentsView);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }
            var commentView = new CommentView(userCommentsView.UserID, userCommentsView.UserName);
            commentView.ViewedDateTime = DateTime.UtcNow;

           await userCommentRepository.UpdateCommentsAsViewed(userCommentsView, commentView);
        }
    }
}
