namespace Whisper.Data.Models.Base;

public abstract class ModelBase : Model
{
    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }
}