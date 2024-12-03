namespace Whisper.Data.Models.Base;

public abstract record Model : IModel
{
    public Guid Id { get; set; }
}