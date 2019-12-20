using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using FluentValidation;

namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public class DeleteCommentValidator : BaseValidator<DeleteComment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommentValidator"/> class.
        /// </summary>
        public DeleteCommentValidator()
        {
            RuleFor(p => p.ModuleID).NotEmpty().WithMessage(ExceptionMessages.ModuleIDEmpty);
            RuleFor(p => p.TopicName).NotEmpty().WithMessage(ExceptionMessages.TopicNameEmpty);
        }
    }
}
