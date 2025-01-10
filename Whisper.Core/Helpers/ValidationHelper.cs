using FluentValidation;
using Whisper.Data.Utils;
using Whisper.Data.Validations;

namespace Whisper.Core.Helpers;

public static class ValidationHelper
{
    public static ServiceResponse<List<ValidationErrorModel>> IsValid<T>(IValidator<T> validator, T entry)
    {
        var validationResults = validator.Validate(entry);

        if (!validationResults.IsValid)
        {
            var errors = validationResults.Errors.Select(failure => new ValidationErrorModel
            (
                failure.PropertyName,
                failure.ErrorMessage
            )).ToList();

            return new ServiceResponse<List<ValidationErrorModel>>
            {
                Success = false,
                StatusCode = 400,
                Data = errors,
                Message = "Validation errors"

            };
        }
        return new ServiceResponse<List<ValidationErrorModel>>
        {
            Success = true,
        };
    }
}
