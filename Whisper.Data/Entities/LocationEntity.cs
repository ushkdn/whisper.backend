using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.Location)]
[PrimaryKey(nameof(Id))]
public record LocationEntity : Entity
{
    [Column("country")]
    [MinLength(5, ErrorMessage = "Country must be at least 5 characters.")]
    [MaxLength(25, ErrorMessage = "Country must be no more than 5 characters.")]
    public required string Country { get; set; }

    [Column("region")]
    [MinLength(5, ErrorMessage = "Region must be at least 5 characters.")]
    [MaxLength(25, ErrorMessage = "Region must be no more than 25 characters.")]
    public required string Region { get; set; }

    [Column("city")]
    [MinLength(3, ErrorMessage = "City must be at least 3 characters.")]  
    [MaxLength(25, ErrorMessage = "City must be no more than 25 characters.")]
    public required string City { get; set; }
}