using System.ComponentModel.DataAnnotations.Schema;

namespace Whisper.Data.Entities.Base;

public abstract record EntityBase : Entity
{
    [Column("date_created")]
    public DateTime DateCreated { get; init; }

    [Column("date_updated")]
    public DateTime DateUpdated { get; init; }
}