using System;
using System.Collections.Generic;
using System.Text;
using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using FluentValidation;
namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public class AddCommentEntryValidator : BaseValidator<AddCommentEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentEntryValidator"/> class.
        /// </summary>
        public AddCommentEntryValidator()
        {
            RuleFor(p => p.ModuleID).NotEmpty().WithMessage(ExceptionMessages.ModuleIDEmpty);
            RuleFor(p => p.ModuleName).NotEmpty().WithMessage(ExceptionMessages.ModuleNameEmpty);
            RuleFor(p => p.ModuleVersion).NotEmpty().WithMessage(ExceptionMessages.ModuleVersionEmpty);
            RuleFor(p => p.TopicName).NotEmpty().WithMessage(ExceptionMessages.TopicNameEmpty);
            RuleFor(p => p.CommentText).NotEmpty().WithMessage(ExceptionMessages.CommentTextEmpty);
            RuleFor(p => p.CreatedUser).NotEmpty().WithMessage(ExceptionMessages.CreatedUserEmpty);
            RuleFor(p => p.CreatedUserID).NotEmpty().WithMessage(ExceptionMessages.CreatedUserIDEmpty);
        }
    }
}
