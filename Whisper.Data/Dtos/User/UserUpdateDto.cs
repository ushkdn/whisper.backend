using System.ComponentModel.DataAnnotations;

namespace Whisper.Data.Dtos.User;

public record UserUpdateDto
{
    public string? Surname { get; init; }

    public string? Name { get; init; }
    
    [MinLength(4, ErrorMessage = "Username cannot be less than 4 characters.")]
    [MaxLength(15, ErrorMessage = "Username cannot be longer than 15 characters")]
    public string? Username { get; init; }
 
    [MinLength(11, ErrorMessage = "Phone number cannot be less than 11 characters")]
    [MaxLength(11, ErrorMessage = "Phone number cannot be longer than 11 characters")]
    public string? PhoneNumber { get; init; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; init; }
    
    
    [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
    [MaxLength(15, ErrorMessage = "Password cannot be longer than 15 characters")]
    public string? Password { get; init; }
    
    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; init; }

    public DateTime? BirthDay { get; init; }
}