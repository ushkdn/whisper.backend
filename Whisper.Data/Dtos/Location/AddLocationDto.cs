using System.ComponentModel.DataAnnotations;

namespace Whisper.Data.Dtos.Location;

public record AddLocationDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Country must be at least 5 characters.")]
    [MaxLength(25, ErrorMessage = "Country must be no more than 5 characters.")]
    public required string Country { get; init; }
}