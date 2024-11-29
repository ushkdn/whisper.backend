using Whisper.Data.Dtos.User;
using Whisper.Data.Utils;

namespace Whisper.Services.UserService;

public class UserService : IUserService
{
    public Task<ServiceResponse<string>> ForgotPassword(UserForgotPasswordDto user)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> LogIn(UserLogInDto user)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> Register(UserRegisterDto user)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> ResetPassword(UserResetPasswordDto user)
    {
        throw new NotImplementedException();
    }
}