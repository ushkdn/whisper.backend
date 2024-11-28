namespace Whisper.Data.Utils;

public record ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public ushort StatusCode { get; set; }
}