using System.ComponentModel.DataAnnotations.Schema;

namespace Whisper.Data.Entities.Base;

public abstract record Entity : IEntity
{
    [Column("id")]
    public int Id { get; init; }
}