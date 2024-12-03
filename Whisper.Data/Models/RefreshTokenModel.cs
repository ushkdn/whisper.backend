using Whisper.Data.Models.Base;

namespace Whisper.Data.Models;

public record RefreshTokenModel : ModelBase
{
    public string? Token { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public virtual UserModel? User { get; set; }
}