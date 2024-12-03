namespace Whisper.Data.Models.Base;

public abstract record ModelBase : Model
{
    public required DateTime DateCreated { get; set; }

    public required DateTime DateUpdated { get; set; }
}