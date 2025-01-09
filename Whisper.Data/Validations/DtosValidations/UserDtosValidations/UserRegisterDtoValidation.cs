using FluentValidation;
using Whisper.Data.Dtos.User;
using Whisper.Data.Validations.DtosValidations.LocationDtoValidations;

namespace Whisper.Data.Validations.DtosValidations.UserDtosValidations;

public class UserRegisterDtoValidation : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidation()
    {
        RuleFor(p => p.Surname)
            .NotEmpty()
            .WithMessage("Surname cannot be empty.")
            .WithErrorCode("400")

            .Length(2, 20)
            .WithMessage("Surname cannot be less than 2 and more than 20 characters.")
            .WithErrorCode("400");

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .WithErrorCode("400")

            .Length(2, 20)
            .WithMessage("Name cannot be less than 2 and more than 20 characters.")
            .WithErrorCode("400");

        RuleFor(p => p.Username)
            .NotEmpty()
            .WithMessage("Username cannot be empty.")
            .WithErrorCode("400")

            .Length(5, 20)
            .WithMessage("Username cannot be less than 5 and more than 20 characters.")
            .WithErrorCode("400");

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

        RuleFor(p => p.ConfirmPassword)
            .Equal(p => p.Password)
            .WithMessage("Passwords do not match.")
            .WithErrorCode("400");

        RuleFor(p => p.Birthday)
            .LessThan(DateTime.UtcNow)
            .WithMessage("Birthday must be less than the current date.")
            .WithErrorCode("400");

        RuleFor(p => p.Location).SetValidator(new AddLocationDtoValidation());
    }
}