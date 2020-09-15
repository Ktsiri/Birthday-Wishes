using IOCO.BirthdayWishes.Contract.DomainLogic.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCO.BirthdayWishes.Dto;
using FluentValidation;

namespace IOCO.BirthdayWishes.DomainLogic.Rules
{
    sealed class InternalValidator<TValidator, TObjectType> : IInternalValidator<TObjectType>
        where TValidator : AbstractValidator<TObjectType>
    {
        private readonly TValidator _validator;

        public InternalValidator(TValidator validator)
        {
            _validator = validator;
        }
        public async Task<ValidationResponseDto> ValidateWithResultAsync(TObjectType objectToValidate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = new ValidationResponseDto();
            if (objectToValidate == null)
                throw new ArgumentNullException(nameof(objectToValidate), "Cannot pass null to Validate.");

            var validationResult = await _validator.ValidateAsync(objectToValidate, cancellationToken);

            response.IsValid = validationResult.IsValid;
            var errors = new List<Error>();
            errors.AddRange(validationResult.Errors.Select(error => new Error
            {
                ErrorCode = error.ErrorCode,
                ErrorMessage = error.ErrorMessage,
                PropertyName = error.PropertyName
            }));
            response.Errors = errors;

            return response;
        }
    }
}
