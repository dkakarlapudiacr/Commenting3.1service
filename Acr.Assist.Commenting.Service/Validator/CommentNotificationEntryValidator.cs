using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using FluentValidation;

namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public class CommentNotificationEntryValidator : BaseValidator<CommentNotificationEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentNotificationEntryValidator"/> class.
        /// </summary>
        public CommentNotificationEntryValidator()
        {
            RuleFor(par => par.EmailID).NotEmpty().EmailAddress().WithMessage(ExceptionMessages.EmailIDEmpty);
            RuleFor(par => par.EmailID).EmailAddress().WithMessage(ExceptionMessages.EmailIDInvalid);
            RuleFor(par => par.User).NotEmpty().WithMessage(ExceptionMessages.UserEmpty);
            RuleFor(par => par.UserID).NotEmpty().WithMessage(ExceptionMessages.UserIDEmpty);
           
        }
    }
}
