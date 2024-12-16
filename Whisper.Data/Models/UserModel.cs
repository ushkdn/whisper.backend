using Whisper.Data.Models.Base;

namespace Whisper.Data.Models;

public record UserModel : ModelBase
{
    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime BirthDay { get; set; }

    public bool IsVerified { get; set; }

    public virtual LocationModel? Location { get; set; }
    public virtual RefreshTokenModel? RefreshToken { get; set; }
}