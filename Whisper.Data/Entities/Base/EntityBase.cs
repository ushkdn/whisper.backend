namespace Whisper.Data.Entities.Base;

public abstract record EntityBase : Entity
{
    public DateTime DateCreated { get; init; }
    public DateTime DateUpdated { get; set; }
}