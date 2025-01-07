using FluentValidation;
using Whisper.Data.Dtos.User;

namespace Whisper.Data.Validations.DtosValidations.UserDtosValidations;

public class UserLogInDtoValidation : AbstractValidator<UserLogInDto>
{
    public UserLogInDtoValidation()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .WithErrorCode("400")

            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email format is invalid.")
            .WithErrorCode("400")

            .Length(5, 20)
            .WithMessage("Email cannot be less than 5 and more than 50 characters.")
            .WithErrorCode("400");

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .WithErrorCode("400")

            .Length(5, 20)
            .WithMessage("Password cannot be less than 5 and more than 20 characters.")
            .WithErrorCode("400");
    }
}