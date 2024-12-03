using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

[Table(Tables.GROUP)]
[PrimaryKey(nameof(Id))]
public record GroupEntity : EntityBase
{
    [Column("title")]
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title cannot be less than 5 characters.")]
    [MaxLength(20, ErrorMessage = "Title cannot be longer than 20 characters.")]
    public required string Title { get; set; }

    [Column("description")]
    [Required(ErrorMessage = "Description is required")]
    [MinLength(10, ErrorMessage = "Description cannot be less than 5 characters.")]
    public required string Description { get; set; }

    [Column("is_closed")]
    [Required(ErrorMessage = "Group status(open/closed) is required")]
    public bool IsClosed { get; set; }
}