using System.ComponentModel.DataAnnotations;

namespace Whisper.Data.Dtos.User;

public record UserForgotPasswordDto
{
    [Required(ErrorMessage = "Email or phone number is required")]
    public required string EmailOrPhoneNumber { get; init; }
}