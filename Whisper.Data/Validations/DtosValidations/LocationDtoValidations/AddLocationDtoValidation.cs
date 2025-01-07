using FluentValidation;
using Whisper.Data.Dtos.Location;

namespace Whisper.Data.Validations.DtosValidations.LocationDtoValidations;

public class AddLocationDtoValidation : AbstractValidator<AddLocationDto>
{
    public AddLocationDtoValidation()
    {
        RuleFor(p => p.Country)
            .NotNull()
            .WithMessage("County cannot be null.")
            .WithErrorCode("400")

            .Length(5, 25)
            .WithMessage("Country cannot be less than 5 and more than 25 characters.")
            .WithErrorCode("400");
    }
}