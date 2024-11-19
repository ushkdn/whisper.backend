using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.User)]
[PrimaryKey(nameof(Id))]
public record UserEntity : EntityBase
{
    [Column("surname")]
    public string Surname { get; set; } = string.Empty;

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("birtday")]
    public DateTime BirthDay { get; init; }

    [ForeignKey(nameof(Location.Id))]
    public virtual LocationEntity? Location { get; set; }

    public virtual List<GroupEntity> Groups { get; set; } = new();
}