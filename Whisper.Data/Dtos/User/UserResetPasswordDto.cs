using System.ComponentModel.DataAnnotations;

namespace Whisper.Data.Dtos.User
{
    public record UserResetPasswordDto
    {
        [Required(ErrorMessage = "Confirmation code is required")]
        public required string EmailOrPhoneCode { get; init; }
        
        [Required(ErrorMessage = "New password is required")]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
        [MaxLength(15, ErrorMessage = "Password cannot be longer than 15 characters")]
        public required string NewPassword { get; init; }
    }
}