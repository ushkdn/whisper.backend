namespace Whisper.Data.Dtos.User;

public record UserResetPasswordDto(string Password, string ConfirmPassword, string SecretCode);