using Whisper.Data.Dtos.User;

namespace Whisper.Services.UserService;

public class UserService : IUserService
{
    public Task ForgotPassword(UserForgotPasswordDto user)
    {
        throw new NotImplementedException();
    }

    public Task LogIn(UserLogInDto user)
    {
        throw new NotImplementedException();
    }

    public Task Register(UserRegisterDto user)
    {
        throw new NotImplementedException();
    }

    public Task ResetPassword(UserResetPasswordDto user)
    {
        throw new NotImplementedException();
    }
}