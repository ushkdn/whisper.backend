using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.Group)]
[PrimaryKey(nameof(Id))]
public record GroupEntity : EntityBase
{
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("is_closed")]
    public bool IsClosed { get; set; }

    public virtual List<UserEntity> Followers { get; set; } = new();
}