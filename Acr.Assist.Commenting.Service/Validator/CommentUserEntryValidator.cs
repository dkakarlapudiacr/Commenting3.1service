using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using FluentValidation;

namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public class CommentUserEntryValidator : BaseValidator<CommentUserEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentUserEntryValidator"/> class.
        /// </summary>
        public CommentUserEntryValidator()
        {
            RuleFor(p => p.User).NotEmpty().WithMessage(ExceptionMessages.UserEmpty);
            RuleFor(p => p.UserID).NotEmpty().WithMessage(ExceptionMessages.UserIDEmpty);
        }
    }
}
