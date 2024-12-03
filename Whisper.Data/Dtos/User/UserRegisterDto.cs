using System.ComponentModel.DataAnnotations;
using Whisper.Data.Dtos.Location;

namespace Whisper.Data.Dtos.User;

public record UserRegisterDto
{
    [Required(ErrorMessage = "Surname is required")]
    public required string Surname { get; init; }

    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; init; }

    [MinLength(4, ErrorMessage = "Username cannot be less than 4 characters")]
    [MaxLength(15, ErrorMessage = "Username cannot be longer than 15 characters")]
    public required string Username { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
    [MaxLength(15, ErrorMessage = "Password cannot be longer than 15 characters")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public required string ConfirmPassword { get; init; }

    [Required(ErrorMessage = "Birthday is required")]
    public DateTime Birthday { get; init; }

    [Required]
    public required AddLocationDto Location { get; init; }
}