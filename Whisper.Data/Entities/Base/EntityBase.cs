namespace Whisper.Data.Entities.Base;

public abstract record EntityBase : Entity
{
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}