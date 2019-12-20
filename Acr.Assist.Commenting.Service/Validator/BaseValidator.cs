using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acr.Assist.CommentMicroService.Service.Validator
{
    public abstract class BaseValidator<T> : AbstractValidator<T>, IDataValidator<T> where T : class
    {
        /// <summary>
        /// Calidates an instance and returns the error messages as Key Value pairs
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>
        /// True if validation succeeds , else it returs false
        /// </returns>
        public ValidatorResult ValidateInstance(T instance)
        {
            ValidatorResult validatorResult = new ValidatorResult();
            var results = Validate(instance);

            validatorResult.IsValid = results.IsValid;
            if (!validatorResult.IsValid)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    validatorResult.Errors.Add(failure.PropertyName, failure.ErrorMessage);
                }
            }
            return validatorResult;
        }
    }
}
