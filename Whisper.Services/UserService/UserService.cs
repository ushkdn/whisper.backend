using System.Text.RegularExpressions;
using Whisper.Data.Dtos.User;
using Whisper.Data.Entities;
using Whisper.Data.Extensions;
using Whisper.Data.Mapping;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;
using Whisper.Data.Utils;

namespace Whisper.Services.UserService;

public class UserService(IUserRepository userRepository, ITransactionManager transactionManager, ICacheRepository cacheRepository) : IUserService
{
    public async Task<ServiceResponse<string>> ForgotPassword(UserForgotPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();
        const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        try
        {
            if (Regex.IsMatch(user.EmailOrPhoneNumber, EMAIL_REGEX))
            {
                //
            }
            else
            {
                //
            }
            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Code for password reset sent";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> LogIn(UserLogInDto user)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var storedUser = await userRepository.GetByEmailOrPhoneNumberAsync(user.EmailOrPhoneNumber);
            if (storedUser.Password != user.Password)
            {
                throw new ArgumentException("Incorrect email/phone-number or password");
            }

            serviceResponse.Success = true;
            serviceResponse.Message = "Log In Success";
            serviceResponse.StatusCode = 201;
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> Register(UserRegisterDto user)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            await userRepository.CreateAsync(WhisperMapper.Mapper.Map<UserEntity>(user));
            await transactionManager.SaveChangesAsync();
            serviceResponse.Success = true;
            serviceResponse.StatusCode = 201;
            serviceResponse.Message = "Your account has been created";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> ResetPassword(UserResetPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Password changed";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return serviceResponse;
    }
}