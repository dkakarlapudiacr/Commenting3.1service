using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using Acr.Assist.CommentMicroService.Service.Validator;
using System;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service
{
    /// <summary>
    /// Class for the comment suggestion related operations for a user
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.ICommentSuggestionService" />
    public class CommentSuggestionService : ICommentSuggestionService
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly ICommentRepository commentRepository;

        /// <summary>
        /// The comment suggestion repository
        /// </summary>
        private readonly ICommentSuggestionRepository commentSuggestionRepository;

        /// <summary>
        /// The comment user entry validator
        /// </summary>
        private readonly IDataValidator<CommentUserEntry> commentUserEntryValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentSuggestionService"/> class.
        /// </summary>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="commentSuggestionRepository">The comment suggestion repository.</param>
        /// <param name="commentUserEntryValidator">The comment user entry validator.</param>
        public CommentSuggestionService(
            ICommentRepository commentRepository, 
            ICommentSuggestionRepository commentSuggestionRepository, 
            IDataValidator<CommentUserEntry> commentUserEntryValidator)
        {
            this.commentRepository = commentRepository;
            this.commentSuggestionRepository = commentSuggestionRepository;
            this.commentUserEntryValidator = commentUserEntryValidator;
        }

        /// <summary>
        /// Adds the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<CommentSuggestion> AddCommentSuggestion(Guid commentID, CommentUserEntry user)
        {
            _CheckCommentIDEmpty(commentID);
            var validatorResult = commentUserEntryValidator.ValidateInstance(user);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }
            await _CheckCommentIfExists(commentID);
            var commentSuggestionEntry = new CommentSuggestion(user.UserID, user.User);
            commentSuggestionEntry.SuggestedDateTime = DateTime.UtcNow;

            await commentSuggestionRepository.AddCommentSuggestion(commentID, commentSuggestionEntry);
            return commentSuggestionEntry;
        }

        /// <summary>
        /// Deletes the comment suggestion.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task DeleteCommentSuggestion(Guid commentID, string userID)
        {
            _CheckCommentIDEmpty(commentID);
            if(string.IsNullOrEmpty(userID) || string.IsNullOrWhiteSpace(userID))
            {
                throw new InputValidationFailureException(ExceptionMessages.UserIDEmpty);
            }

            await _CheckCommentIfExists(commentID);
            await commentSuggestionRepository.DeleteCommentSuggestion(commentID, userID);
        }

        /// <summary>
        /// Checks the comment identifier empty.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        private void _CheckCommentIDEmpty(Guid commentID)
        {
            if (commentID == Guid.Empty || string.IsNullOrEmpty(commentID.ToString()) || string.IsNullOrWhiteSpace(commentID.ToString()))
            {
                throw new InputValidationFailureException(ExceptionMessages.CommentIDEmpty);
            }
        }

        /// <summary>
        /// Checks the comment if exists.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <exception cref="ResourceNotFoundException"></exception>
        private async Task _CheckCommentIfExists(Guid commentID)
        {
            if (!await commentRepository.CheckIfCommentExist(commentID))
            {
                throw new ResourceNotFoundException(string.Format(ExceptionMessages.CommentNotFound, commentID));
            }
        }
    }
}
