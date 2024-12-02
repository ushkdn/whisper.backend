using Whisper.Data.Models.Base;

namespace Whisper.Data.Models;

public record RefreshTokenModel : ModelBase
{
    public required string Token { get; set; }
    public required DateTime ExpiryDate { get; set; }
    public UserModel? User { get; set; }
}