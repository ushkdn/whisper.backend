namespace Whisper.Data.Models.Base;

public abstract record ModelBase : Model
{
    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }
}