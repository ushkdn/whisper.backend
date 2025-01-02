using FluentValidation;
using Whisper.Data.Dtos.User;

namespace Whisper.Data.Validations.DtosValidations.UserDtosValidations;

public class UserRegisterDtoValidation : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidation()
    {
        RuleFor(p => p.Surname)
            .NotEmpty().WithMessage("Property Surname cannot be empty.")
            .MinimumLength(2).WithMessage("Property Surname cannot be less than 2 characters.")
            .MaximumLength(20).WithMessage("Property Surname cannot be more than 20 characters.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Property Name cannot be empty.")
            .MinimumLength(2).WithMessage("Property Name cannot be less than 2 characters.")
            .MaximumLength(20).WithMessage("Property Name cannot be more than 20 characters.");

        RuleFor(p => p.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MinimumLength(5).WithMessage("Username cannot be less than 5 characters.")
            .MaximumLength(20).WithMessage("Username cannot be more than 20 characters.");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Email format is invalid.")
            .MinimumLength(5).WithMessage("Email cannot be less than 5 characters.")
            .MaximumLength(50).WithMessage("Email cannot be more than 50 characters.");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(5).WithMessage("Password cannot be less than 5 characters.")
            .MaximumLength(20).WithMessage("Password cannot be more than 20 characters.");

        RuleFor(p => p.ConfirmPassword)
            .Equal(p => p.Password).WithMessage("Passwords do not match.");

        RuleFor(p => p.Birthday)
            .LessThan(DateTime.UtcNow).WithMessage("Birthday must be less than the current date.");
    }
}