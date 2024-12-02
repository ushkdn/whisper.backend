namespace Whisper.Data.Utils;

public record MessagePayload
{
    public required string User { get; set; }

    public string? Topic { get; set; }

    public string? Message { get; set; }
}