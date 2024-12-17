using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.REFRESH_TOKEN)]
[PrimaryKey(nameof(Id))]
public class RefreshTokenEntity : EntityBase
{
    public string Token { get; set; }

    public DateTime ExpireDate { get; set; }

    public virtual UserEntity? User { get; set; }
}