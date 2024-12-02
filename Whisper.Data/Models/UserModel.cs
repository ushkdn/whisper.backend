using Whisper.Data.Entities;
using Whisper.Data.Models.Base;

namespace Whisper.Data.Models;

public record UserModel : ModelBase
{
    public required string Surname { get; set; }

    public required string Name { get; set; }

    public required string Username { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public DateTime BirthDay { get; set; }

    public required LocationEntity Location { get; set; }
}