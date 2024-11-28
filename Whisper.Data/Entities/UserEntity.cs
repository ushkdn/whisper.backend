using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.User)]
[PrimaryKey(nameof(Id))]
public record UserEntity : EntityBase
{
    [Column("surname")]
    public string Surname { get; set; } = string.Empty;

    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("username")]
    public string Username { get; set; } = string.Empty;

    [Column("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("password")] 
    public string Password { get; set; } = string.Empty;

    [Column("birthday")]
    public DateTime BirthDay { get; init; }
}