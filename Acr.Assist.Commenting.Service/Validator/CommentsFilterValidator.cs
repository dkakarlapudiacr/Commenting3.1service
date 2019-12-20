using Acr.Assist.CommentMicroService.Core.DTO;
using FluentValidation;
using Acr.Assist.CommentMicroService.Service.DataValidation;
namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public class CommentsFilterValidator : BaseValidator<CommentsFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsFilterValidator"/> class.
        /// </summary>
        public CommentsFilterValidator()
        {
            RuleFor(parameter => parameter.TopicID).NotEmpty().WithMessage(ExceptionMessages.TopicIDEmpty);
            RuleFor(parameter => parameter.ModuleID).NotEmpty().WithMessage(ExceptionMessages.ModuleIDEmpty);
            RuleFor(parameter => parameter.TakeCount).NotEmpty().WithMessage(ExceptionMessages.TakeCountEmpty);
        }
    }
}
