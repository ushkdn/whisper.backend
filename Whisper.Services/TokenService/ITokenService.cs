namespace Whisper.Services.TokenService;

public interface ITokenService
{
    Task RefreshToken();
}