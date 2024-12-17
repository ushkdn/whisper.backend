using System.ComponentModel.DataAnnotations.Schema;

namespace Whisper.Data.Entities.Base;

public abstract class Entity : IEntity
{
    [Column("id")]
    public Guid Id { get; set; }
}