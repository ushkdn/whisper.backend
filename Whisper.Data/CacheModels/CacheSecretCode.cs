namespace Whisper.Data.CacheModels;

public record CacheSecretCode
{
    public Guid UserId { get; set; }
    public required string SecretCode { get; set; }
}