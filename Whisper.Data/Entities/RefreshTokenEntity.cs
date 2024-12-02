using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public record RefreshTokenEntity : EntityBase
{
    public required string Token { get; init; }

    public required DateTime ExpireDate { get; init; }

    [ForeignKey("user_id")]
    public virtual UserEntity? User { get; init; }
}