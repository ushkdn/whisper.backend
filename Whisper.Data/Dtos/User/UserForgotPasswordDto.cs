using System.ComponentModel.DataAnnotations;

namespace Whisper.Data.Dtos.User;

public record UserForgotPasswordDto
{
    [Required(ErrorMessage = "Email or phone number is required")]
    public required string EmailOrPhoneNumber { get; init; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
    [MaxLength(15, ErrorMessage = "Password cannot be longer than 15 characters")]
    public required string Password { get; init; }
}