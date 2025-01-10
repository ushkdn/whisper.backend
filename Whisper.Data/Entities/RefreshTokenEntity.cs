using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public class RefreshTokenEntity : EntityBase
{
    public string? Token { get; set; }

    public DateTime ExpireDate { get; set; }

    public virtual UserEntity? User { get; set; }
}