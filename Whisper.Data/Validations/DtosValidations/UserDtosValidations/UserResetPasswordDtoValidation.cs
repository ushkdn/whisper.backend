using FluentValidation;
using Whisper.Data.Dtos.User;

namespace Whisper.Data.Validations.DtosValidations.UserDtosValidations;

public class UserResetPasswordDtoValidation : AbstractValidator<UserResetPasswordDto>
{
    public UserResetPasswordDtoValidation()
    {
        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .WithErrorCode("400")

            .Length(5, 20)
            .WithMessage("Password cannot be less than 5 and more than 20 characters.")
            .WithErrorCode("400");

        RuleFor(p => p.ConfirmPassword)
            .Equal(p => p.Password)
            .WithMessage("Passwords do not match.")
            .WithErrorCode("400");

        RuleFor(p => p.SecretCode)
            .NotEmpty()
            .WithMessage("Secret code cannot be empty.")
            .WithErrorCode("400")

            .Length(8)
            .WithMessage("Secret code must be 8 characters long.")
            .WithErrorCode("400");
    }
}