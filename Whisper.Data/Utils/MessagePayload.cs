namespace Whisper.Data.Utils;

public record MessagePayload
{
    public string? UserEmail { get; set; }

    public int? UserId { get; set; }

    public string? Username { get; set; }

    public string? Topic { get; set; }

    public string? Message { get; set; }
}