using Acr.Assist.CommentMicroService.Core.Data;
using Acr.Assist.CommentMicroService.Core.Domain;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Core.Exception;
using Acr.Assist.CommentMicroService.Core.Infrastructure.Email;
using Acr.Assist.CommentMicroService.Core.Integrations;
using Acr.Assist.CommentMicroService.Core.Services;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using Acr.Assist.CommentMicroService.Service.Validator;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.CommentMicroService.Service
{
    /// <summary>
    /// Class for the commenting related operations
    /// </summary>
    /// <seealso cref="Acr.Assist.CommentMicroService.Core.Services.ICommentService" />
    public class CommentService : ICommentService
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly ICommentRepository commentRepository;

        /// <summary>
        /// The add comment entry validator
        /// </summary>
        private readonly IDataValidator<AddCommentEntry> addCommentEntryValidator;

        /// <summary>
        /// The comments filter validator
        /// </summary>
        private readonly IDataValidator<CommentsFilter> commentsFilterValidator;

        /// <summary>
        /// The comment implement entry validator
        /// </summary>
        private readonly IDataValidator<CommentImplementEntry> commentImplementEntryValidator;

        /// <summary>
        /// The comment user entry validator
        /// </summary>
        private readonly IDataValidator<CommentUserEntry> commentUserEntryValidator;

        /// <summary>
        /// The comment identifier validator
        /// </summary>
        private readonly IDataValidator<CommentIDDetails> commentIDValidator;

        /// <summary>
        /// The delete comment validator
        /// </summary>
        private readonly IDataValidator<DeleteComment> deleteCommentValidator;

        /// <summary>
        /// The email template manager
        /// </summary>
        private readonly IEmailTemplateManager emailTemplateManager;

        /// <summary>
        /// The email notificatio micro service
        /// </summary>
        private readonly IEmailNotificationMicroService emailNotificatioMicroService;

        /// <summary>
        /// The notification sender service
        /// </summary>
        private INotificationSenderService notificationSenderService;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="addCommentEntryValidator">The add comment entry validator.</param>
        /// <param name="commentsFilterValidator">The comments filter validator.</param>
        /// <param name="commentImplementEntryValidator">The comment implement entry validator.</param>
        /// <param name="commentUserEntryValidator">The comment user entry validator.</param>
        /// <param name="commentIDValidator">The comment identifier validator.</param>
        /// <param name="updateCommentUserImagePathValidator">The update comment user image path validator.</param>
        /// <param name="deleteCommentValidator">The delete comment validator.</param>
        /// <param name="emailTemplateManager">The email template manager.</param>
        /// <param name="emailNotificatioMicroService">The email notificatio micro service.</param>
        /// <param name="notificationSenderService">The notification sender service.</param>
        /// <param name="mapper">The mapper.</param>
        public CommentService(
            ICommentRepository commentRepository,
            IDataValidator<AddCommentEntry> addCommentEntryValidator,
            IDataValidator<CommentsFilter> commentsFilterValidator,
            IDataValidator<CommentImplementEntry> commentImplementEntryValidator,
            IDataValidator<CommentUserEntry> commentUserEntryValidator,
            IDataValidator<CommentIDDetails> commentIDValidator,
            IDataValidator<DeleteComment> deleteCommentValidator,
            IEmailTemplateManager emailTemplateManager,
            IEmailNotificationMicroService emailNotificatioMicroService,
            INotificationSenderService notificationSenderService,
            IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.addCommentEntryValidator = addCommentEntryValidator;
            this.commentsFilterValidator = commentsFilterValidator;
            this.commentImplementEntryValidator = commentImplementEntryValidator;
            this.commentUserEntryValidator = commentUserEntryValidator;
            this.commentIDValidator = commentIDValidator;
            this.deleteCommentValidator = deleteCommentValidator;
            this.emailTemplateManager = emailTemplateManager;
            this.emailNotificatioMicroService = emailNotificatioMicroService;
            this.notificationSenderService = notificationSenderService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="addCommentEntry">The add comment entry.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<CommentEntry> AddComment(AddCommentEntry addCommentEntry)
        {
            var validatorResult = addCommentEntryValidator.ValidateInstance(addCommentEntry);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }

            var commentEntry = mapper.Map<CommentEntry>(addCommentEntry);

            commentEntry.CreatedDateTime = DateTime.UtcNow;
            commentEntry.UpdatedDateTime = DateTime.UtcNow;
            commentEntry.Status = CommentStatus.Created;

            commentEntry.CommentID = await commentRepository.AddComment(commentEntry);

            if (addCommentEntry.ToRecipients != null && addCommentEntry.ToRecipients.Count > 0)
            {
                List<EmailMessage> emailsToSend = new List<EmailMessage>();

                foreach (var user in addCommentEntry.ToRecipients)
                {
                    EmailMessage userEmailMessage = emailTemplateManager.GetMentionedEmail(user.FirstName, commentEntry.ModuleName, commentEntry.CommentText,
                                                                                           addCommentEntry.ApplicationUrl, addCommentEntry.ToRecipients);
                    emailsToSend.Add(userEmailMessage);
                }
                var emailSend = Task.Run(async () =>
                {
                    foreach (var email in emailsToSend)
                    {
                        await this.emailNotificatioMicroService.SendEmailNotification(email);
                    }
                });
            }
            
            var commentNotification = new UserUnViewedComment();
            commentNotification.CommentID = commentEntry.CommentID;
            commentNotification.CommentText = commentEntry.CommentText;
            commentNotification.CreatedUser = commentEntry.CreatedUser;
            commentNotification.CreatedUserID = commentEntry.CreatedUserID;
            commentNotification.ModuleID = commentEntry.ModuleID;
            commentNotification.ModuleName = commentEntry.ModuleName;
            commentNotification.TopicName = commentEntry.TopicName;
            commentNotification.UpdatedDateTime = commentEntry.UpdatedDateTime;
            await notificationSenderService.NotifyAsCommentAdded(commentNotification);

            return commentEntry;
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentIDDetails">The comment identifier details.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task DeleteComment(CommentIDDetails commentIDDetails)
        {
            var validatorResult = commentIDValidator.ValidateInstance(commentIDDetails);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }
            await _CheckCommentIfExists(commentIDDetails.CommentID);
            await commentRepository.DeleteComment(commentIDDetails.CommentID);
        }

        /// <summary>
        /// Edits the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentEntry">The comment entry.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<CommentUpdateResult> EditComment(Guid commentID, UpdateCommentEntry commentEntry)
        {
            _CheckCommentIDEmpty(commentID);
            if (string.IsNullOrWhiteSpace(commentEntry.CommentText) || string.IsNullOrEmpty(commentEntry.CommentText))
            {
                throw new InputValidationFailureException(ExceptionMessages.CommentTextEmpty);
            }
            await _CheckCommentIfExists(commentID);
            var commentUpdate = new CommentUpdateResult();
            commentUpdate.CommentID = commentID;
            commentUpdate.CommentText = commentEntry.CommentText;

            commentUpdate.UpdatedDateTime = DateTime.UtcNow;

            await commentRepository.EditComment(commentUpdate);

            if (commentEntry.ToRecipients != null && commentEntry.ToRecipients.Count > 0)
            {
                List<EmailMessage> emailsToSend = new List<EmailMessage>();

                foreach (var user in commentEntry.ToRecipients)
                {
                    EmailMessage userEmailMessage = emailTemplateManager.GetMentionedEmail(user.FirstName, commentEntry.ModuleName, commentEntry.CommentText,
                                                                                           commentEntry.ApplicationUrl, commentEntry.ToRecipients);
                    emailsToSend.Add(userEmailMessage);
                }
                var emailSend = Task.Run(async () =>
                {
                    foreach (var email in emailsToSend)
                    {
                        await this.emailNotificatioMicroService.SendEmailNotification(email);
                    }
                });
            }

            return commentUpdate;
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<List<CommentEntry>> GetComments(CommentsFilter filter)
        {
            var validatorResult = commentsFilterValidator.ValidateInstance(filter);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }

            return await commentRepository.GetComments(filter);
        }

        /// <summary>
        /// Implements the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="commentImplementEntry">The comment implement entry.</param>
        /// <returns></returns>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task<CommentImplementResult> ImplementComment(Guid commentID, CommentImplementEntry commentImplementEntry)
        {
            var validatorResult = commentImplementEntryValidator.ValidateInstance(commentImplementEntry);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }
            await _CheckCommentIfExists(commentID);

            var ImplementedDateTime = DateTime.UtcNow;

            await commentRepository.ImplementComment(commentID, commentImplementEntry, ImplementedDateTime);
            var res = mapper.Map<CommentImplementResult>(commentImplementEntry);
            res.ImplementedDateTime = ImplementedDateTime;

            return res;
        }

        /// <summary>
        /// Proposes the comment.
        /// </summary>
        /// <param name="commentID">The comment identifier.</param>
        /// <param name="userEntry">The user entry.</param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task ProposeComment(Guid commentID, CommentUserEntry userEntry)
        {
            var validatorResult = commentUserEntryValidator.ValidateInstance(userEntry);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }
            _CheckCommentIDEmpty(commentID);
            await _CheckCommentIfExists(commentID);
            var ProposedDateTime = DateTime.UtcNow;
            await commentRepository.ProposeComment(commentID, userEntry, ProposedDateTime);
        }

        /// <summary>
        /// Deletes the comment based on module id and topic id
        /// </summary>
        /// <param name="deleteComment"></param>
        /// <exception cref="InputValidationFailureException"></exception>
        public async Task DeleteTopicComments(DeleteComment deleteComment)
        {
            var validatorResult = deleteCommentValidator.ValidateInstance(deleteComment);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }

            await commentRepository.DeleteTopicComments(deleteComment);
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
            if (! await commentRepository.CheckIfCommentExist(commentID))
            {
                throw new ResourceNotFoundException(string.Format(ExceptionMessages.CommentNotFound, commentID));
            }
        }
    }
}
