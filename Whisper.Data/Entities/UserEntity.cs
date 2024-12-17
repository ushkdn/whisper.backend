using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.USER)]
[PrimaryKey(nameof(Id))]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(PhoneNumber), IsUnique = true)]
public class UserEntity : EntityBase
{
    [Column("surname")]
    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; }

    [Column("name")]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Column("username")]
    [MinLength(4, ErrorMessage = "Username cannot be less than 4 characters")]
    [MaxLength(15, ErrorMessage = "Username cannot be longer than 15 characters")]
    public string Username { get; set; }

    [Column("phone_number")]
    [MinLength(11, ErrorMessage = "Phone number cannot be less than 11 characters")]
    [MaxLength(11, ErrorMessage = "Phone number cannot be longer than 11 characters")]
    public string? PhoneNumber { get; set; }

    [Column("email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Column("password")]
    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
    //max - 120 coz hashed password string
    [MaxLength(120, ErrorMessage = "Password cannot be longer than 120 characters")]
    public string Password { get; set; }

    [Column("birthday")]
    [Required(ErrorMessage = "Birthday is required")]
    public DateTime BirthDay { get; set; }

    [Column("is_verified")]
    public bool IsVerified { get; set; } = false;

    [ForeignKey("location_id")]
    public virtual LocationEntity? Location { get; set; }

    [ForeignKey("refresh_token_id")]
    public virtual RefreshTokenEntity? RefreshToken { get; set; }
}