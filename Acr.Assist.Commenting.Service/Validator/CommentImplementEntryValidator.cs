using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using FluentValidation;
namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public class CommentImplementEntryValidator : BaseValidator<CommentImplementEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentImplementEntryValidator"/> class.
        /// </summary>
        public CommentImplementEntryValidator()
        {
            RuleFor(par => par.ImplementedUserID).NotEmpty().WithMessage(ExceptionMessages.ImplementedUserIDEmpty);
            RuleFor(par => par.ImplementedUser).NotEmpty().WithMessage(ExceptionMessages.ImplementedUserEmpty);
            RuleFor(par => par.ImplementedComment).NotEmpty().WithMessage(ExceptionMessages.ImplementedCommentEmpty);
            RuleFor(par => par.ImplementedModuleVersion).NotEmpty().WithMessage(ExceptionMessages.ImplementedModuleVersionEmpty);
        }
    }
}
