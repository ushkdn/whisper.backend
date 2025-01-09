namespace Whisper.Data.Models.Base;

public abstract class Model : IModel
{
    public Guid Id { get; set; }
}