using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using Acr.Assist.CommentMicroService.Service.Validator;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service
{
    /// <summary>
    ///  Class for the comment noticaition related operations for a user
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.ICommentNotificationService" />
    public class CommentNotificationService : ICommentNotificationService
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly ICommentRepository commentRepository;

        /// <summary>
        /// The comment notification repository
        /// </summary>
        private readonly ICommentNotificationRepository commentNotificationRepository;

        /// <summary>
        /// The comment notification entry validator
        /// </summary>
        private readonly IDataValidator<CommentNotificationEntry> commentNotificationEntryValidator;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentNotificationService"/> class.
        /// </summary>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="commentNotificationRepository">The comment notification repository.</param>
        /// <param name="commentNotificationEntryValidator">The comment notification entry validator.</param>
        /// <param name="mapper">The mapper.</param>
        public CommentNotificationService(
            ICommentRepository commentRepository, 
            ICommentNotificationRepository commentNotificationRepository,
            IDataValidator<CommentNotificationEntry> commentNotificationEntryValidator,
            IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.commentNotificationRepository = commentNotificationRepository;
            this.commentNotificationEntryValidator = commentNotificationEntryValidator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Adds the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="notificationEntry">The notification entry.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task AddCommentNotification(Guid commentID, CommentNotificationEntry notificationEntry)
        {
            _CheckCommentIDEmpty(commentID);
            var validatorResult = commentNotificationEntryValidator.ValidateInstance(notificationEntry);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }

            await _CheckCommentIfExists(commentID);

            var commentNotificationEntry = mapper.Map<CommentNotification>(notificationEntry);
            commentNotificationEntry.NotifiedDateTime = DateTime.UtcNow;
            await commentNotificationRepository.AddCommentNotification(commentID, commentNotificationEntry);
        }

        /// <summary>
        /// Deletes the comment notification.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task DeleteCommentNotification(Guid commentID, string userID)
        {

            _CheckCommentIDEmpty(commentID);
            if (string.IsNullOrEmpty(userID) || string.IsNullOrWhiteSpace(userID))
            {
                throw new InputValidationFailureException(ExceptionMessages.UserIDEmpty);
            }
            await _CheckCommentIfExists(commentID);

            await commentNotificationRepository.DeleteCommentNotification(commentID, userID);
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
