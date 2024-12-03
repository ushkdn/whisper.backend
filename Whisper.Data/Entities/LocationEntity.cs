using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.LOCATION)]
[PrimaryKey(nameof(Id))]
[Index(nameof(Country), IsUnique = true)]
public record LocationEntity : Entity
{
    [Column("country")]
    [MinLength(5, ErrorMessage = "Country must be at least 5 characters.")]
    [MaxLength(25, ErrorMessage = "Country must be no more than 5 characters.")]
    public required string Country { get; set; }

    [ForeignKey("user_id")]
    public virtual List<UserEntity>? User { get; set; }
}