using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public record RefreshTokenEntity : EntityBase
{
    public required string Token { get; init; }

    public required DateTime ExpireDate { get; init; }

    [ForeignKey(nameof(User.Id))]
    public required UserEntity User { get; init; }
}