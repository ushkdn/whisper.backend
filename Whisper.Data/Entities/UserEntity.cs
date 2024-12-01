using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.User)]
[PrimaryKey(nameof(Id))]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(PhoneNumber), IsUnique = true)]
public record UserEntity : EntityBase
{
    [Column("surname")]
    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; } = string.Empty;

    [Column("name")]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Column("username")]
    [MinLength(4, ErrorMessage = "Username cannot be less than 4 characters")]
    [MaxLength(15, ErrorMessage = "Username cannot be longer than 15 characters")]
    public string Username { get; set; } = string.Empty;

    [Column("phone_number")]
    [Required(ErrorMessage = "Phone number is required")]
    [MinLength(11, ErrorMessage = "Phone number cannot be less than 11 characters")]
    [MaxLength(11, ErrorMessage = "Phone number cannot be longer than 11 characters")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Column("email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Column("password")]
    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
    [MaxLength(15, ErrorMessage = "Password cannot be longer than 15 characters")]
    public string Password { get; set; } = string.Empty;

    [Column("birthday")]
    [Required(ErrorMessage = "Birthday is required")]
    public DateTime BirthDay { get; init; }

    public LocationEntity? Location { get; set; }
}