using FluentValidation;
using Whisper.Data.Validations;

namespace Whisper.Core.Helpers;

public static class ValidationHelper
{
    public static List<ValidationError>? Validate<T>(IValidator<T> validator, T entry)
    {
        var validationResults = validator.Validate(entry);

        if (!validationResults.IsValid)
        {
            var errors = validationResults.Errors.Select(failure => new ValidationError
            (
                failure.PropertyName,
                failure.ErrorMessage
            )).ToList();

            return errors;
        }
        return null;
    }
}