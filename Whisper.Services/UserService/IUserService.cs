namespace Whisper.Services.UserService;

public interface IUserService
{
    Task Register(UserRegisterDto user);

    Task LogIn(UserLogInDto user);

    Task ForgotPassword(UserForgotPasswordDto user);

    Task ResetPassword(UserResetPasswordDto user);
}