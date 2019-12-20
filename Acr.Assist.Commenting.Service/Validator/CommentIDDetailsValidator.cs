using Acr.Assist.CommentMicroService.Core.DTO;
using Acr.Assist.CommentMicroService.Service.DataValidation;
using FluentValidation;
namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public class CommentIDDetailsValidator : BaseValidator<CommentIDDetails>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentIDDetailsValidator"/> class.
        /// </summary>
        public CommentIDDetailsValidator()
        {
            RuleFor(ipar => ipar.CommentID).NotEmpty().WithMessage(ExceptionMessages.CommentIDEmpty);
        }
    }
}
