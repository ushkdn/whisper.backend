using Whisper.Data.Models.Base;

namespace Whisper.Data.Models;

public record RefreshTokenModel : ModelBase
{
    public required string Token { get; set; }
    public required DateTime ExpireDate { get; set; }
    public virtual UserModel? User { get; set; }
}