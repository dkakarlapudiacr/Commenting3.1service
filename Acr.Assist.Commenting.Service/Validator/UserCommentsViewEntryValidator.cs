using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using FluentValidation;
namespace Acr.Assist.CommentMicroService.Service.Validator
{
   public class UserCommentsViewEntryValidator : BaseValidator<UserCommentsViewEntry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommentsViewEntryValidator"/> class.
        /// </summary>
        public UserCommentsViewEntryValidator()
        {
            RuleFor(prop => prop.ModuleID).NotEmpty().WithMessage(ExceptionMessages.ModuleIDEmpty);
            RuleFor(prop => prop.TopicID).NotEmpty().WithMessage(ExceptionMessages.TopicIDEmpty);
            RuleFor(prop => prop.UserID).NotEmpty().WithMessage(ExceptionMessages.UserIDEmpty);
            RuleFor(prop => prop.UserName).NotEmpty().WithMessage(ExceptionMessages.UserNameEmpty);           
        }
    }
}
