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
    /// Class for the comment view related operations for a user
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.ICommentViewService" />
    public class CommentViewService : ICommentViewService
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly ICommentRepository commentRepository;

        /// <summary>
        /// The comment view repository
        /// </summary>
        private readonly ICommentViewRepository commentViewRepository;

        /// <summary>
        /// The comment user entry validator
        /// </summary>
        private readonly IDataValidator<CommentUserEntry> commentUserEntryValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewService"/> class.
        /// </summary>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="commentViewRepository">The comment view repository.</param>
        /// <param name="commentUserEntryValidator">The comment user entry validator.</param>
        public CommentViewService(
            ICommentRepository commentRepository,
            ICommentViewRepository commentViewRepository,
             IDataValidator<CommentUserEntry> commentUserEntryValidator)
        {
            this.commentRepository = commentRepository;
            this.commentViewRepository = commentViewRepository;
            this.commentUserEntryValidator = commentUserEntryValidator;
        }

        /// <summary>
        /// Adds the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userEntry">The user entry.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task AddCommentView(Guid commentID, CommentUserEntry userEntry)
        {
            _CheckCommentIDEmpty(commentID);
            var validatorResult = commentUserEntryValidator.ValidateInstance(userEntry);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }
            await _CheckCommentIfExists(commentID);

            var commentViewEntry = new CommentView(userEntry.UserID, userEntry.User);
            commentViewEntry.ViewedDateTime = DateTime.UtcNow;
            await commentViewRepository.AddCommentView(commentID, commentViewEntry);
        }

        /// <summary>
        /// Deletes the comment view.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task DeleteCommentView(Guid commentID, string userID)
        {
            _CheckCommentIDEmpty(commentID);
            if (string.IsNullOrEmpty(userID) || string.IsNullOrWhiteSpace(userID))
            {
                throw new InputValidationFailureException(ExceptionMessages.UserIDEmpty);
            }

            await _CheckCommentIfExists(commentID);

            await commentViewRepository.DeleteCommentView(commentID, userID);
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
