using System.Text.RegularExpressions;
using Whisper.Data.Dtos.User;
using Whisper.Data.Entities;
using Whisper.Data.Extensions;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;
using Whisper.Data.Utils;

namespace Whisper.Services.UserService;

public class UserService(IUserRepository userRepository, ITransactionManager transactionManager, ICacheRepository cacheRepository) : IUserService
{
    private const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public async Task<ServiceResponse<string>> ForgotPassword(UserForgotPasswordDto request)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            if (Regex.IsMatch(request.EmailOrPhoneNumber, EMAIL_REGEX))
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

    public async Task<ServiceResponse<string>> LogIn(UserLogInDto request)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            UserEntity storedUser = null;
            if (Regex.IsMatch(request.EmailOrPhoneNumber, EMAIL_REGEX))
            {
                storedUser = await userRepository.GetByEmailAsync(request.EmailOrPhoneNumber);
            }
            else
            {
                storedUser = await userRepository.GetByPhoneNumberAsync(request.EmailOrPhoneNumber);
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, storedUser.Password))
            {
                throw new ArgumentException("Wrong email/phonenumber or password.");
            }

            if (!storedUser.IsVerified)
            {
                throw new InvalidOperationException("Your account is not verified.");
            }

            //token zone should be here

            serviceResponse.Success = true;
            serviceResponse.Message = "Your are logged in";
            serviceResponse.StatusCode = 201;
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> Register(UserRegisterDto request)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            //remove all not verified accs via quartz every 1h??
            //var storedUser = await userRepository.GetByEmailAndPhoneNumberAsync(request.Email, request.PhoneNumber)
            //  ?? throw new ArgumentException("User is already registered.");

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await userRepository.CreateAsync(WhisperMapper.Mapper.Map<UserEntity>(request));
            await transactionManager.SaveChangesAsync();
            //mail send here
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