using System.ComponentModel.DataAnnotations.Schema;

namespace Whisper.Data.Entities.Base;

public abstract class EntityBase : Entity
{
    [Column("date_created")]
    public DateTime DateCreated { get; set; }

    [Column("date_updated")]
    public DateTime DateUpdated { get; set; }
}