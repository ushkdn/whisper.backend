using Whisper.Data.Models.Base;

namespace Whisper.Data.Models;

public class RefreshTokenModel : ModelBase
{
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
    public virtual UserModel? User { get; set; }
}