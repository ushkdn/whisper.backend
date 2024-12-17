using Whisper.Core.Helpers;
using Whisper.Data;
using Whisper.Data.CacheModels;
using Whisper.Data.Entities;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;
using Whisper.Data.Utils;
using Whisper.Services.MessageService;
using Whisper.Services.TokenService;

namespace Whisper.Services.AuthService;

public class AuthService(
    IUserRepository userRepository,
    ITransactionManager transactionManager,
    ICacheRepository cacheRepository,
    IMessageService emailService,
    ITokenService tokenService
    ) : IAuthService
{
    public async Task ForgotPassword(string email)
    {
        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetByEmailAsync(email))
            ?? throw new KeyNotFoundException($"Unable to find user by email: {email}");

        var msgPayload = new MessagePayload
        {
            UserEmail = email,
            Topic = "Secret code for password reset",
            Message = CodeGenerator.GenerateSecurityCode(),
        };
        await emailService.SendMessage(msgPayload);

        await cacheRepository.SetSingleAsync(
            CacheTables.SECRET_CODE + $":{msgPayload.UserEmail}",
            new CacheSecretCode
            {
                SecretCode = msgPayload.Message
            },
            DateTimeOffset.UtcNow.AddMinutes(5)
        );
    }

    public async Task<TokensModel> LogIn(string email, string password)
    {
        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetRelatedByEmailAsync(email))
            ?? throw new ArgumentException("Wrong email or password.");

        if (!BCrypt.Net.BCrypt.Verify(password, storedUser?.Password))
        {
            throw new ArgumentException("Wrong email/phonenumber or password.");
        }

        if (!storedUser.IsVerified)
        {
            throw new InvalidOperationException("Your account is not verified.");
        }

        var refreshToken = tokenService.CreateRefreshToken();
        var accessToken = tokenService.CreateAccessToken(storedUser);
        tokenService.SetRefreshToken(refreshToken);

        storedUser.RefreshToken = refreshToken;

        userRepository.Update(WhisperMapper.Mapper.Map<UserEntity>(storedUser));

        await transactionManager.SaveChangesAsync();

        return new TokensModel
        {
            RefreshToken = refreshToken.Token,
            AccessToken = accessToken
        };
    }

    public async Task Register(UserModel user)
    {
        //remove all not verified accs via quartz every 1h??
        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetByEmailAsync(user.Email));
        if (storedUser is not null)
        {
            throw new InvalidOperationException("User already exists.");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        await userRepository.CreateAsync(WhisperMapper.Mapper.Map<UserEntity>(user));
        await transactionManager.SaveChangesAsync();

        var msgPayload = new MessagePayload
        {
            UserEmail = user.Email,
            Topic = "Secret code for account verification",
            Message = CodeGenerator.GenerateSecurityCode(),
        };
        await emailService.SendMessage(msgPayload);

        await cacheRepository.SetSingleAsync(
            CacheTables.SECRET_CODE + $":{msgPayload.UserEmail}",
            new CacheSecretCode
            {
                SecretCode = msgPayload.Message
            },
            DateTimeOffset.UtcNow.AddMinutes(5)
        );
    }

    public async Task ResetPassword(Guid userId, string password, string secretCode)
    {
        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetByIdAsync(userId))
            ?? throw new KeyNotFoundException($"Unable to find user by id: {userId}");

        if (!storedUser.IsVerified)
        {
            throw new InvalidOperationException("You have not verified your account.");
        }

        var cachedCode = await cacheRepository.GetSingleAsync<CacheSecretCode>(CacheTables.SECRET_CODE + $":{userId}")
            ?? throw new ArgumentException("Your code for password reset expired. Please try again.");

        if (secretCode != cachedCode.SecretCode)
        {
            throw new ArgumentException("Invalid secret code.");
        }

        storedUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
        userRepository.Update(WhisperMapper.Mapper.Map<UserEntity>(storedUser));
        await transactionManager.SaveChangesAsync();
    }

    public async Task<TokensModel> Verify(Guid userId, string secretCode)
    {
        var cachedSecretCode = await cacheRepository.GetSingleAsync<CacheSecretCode>
        (
            CacheTables.SECRET_CODE + $":{userId}"
        );

        if (cachedSecretCode is null)
        {
            throw new ArgumentNullException("Your secret code expires. Please try again.");
        }

        if (secretCode != cachedSecretCode.SecretCode)
        {
            throw new ArgumentException("Wrong email or secret code.");
        }

        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetRelatedByIdAsync(userId))
            ?? throw new KeyNotFoundException($"Unable to find user by id: {userId}");

        if (storedUser.IsVerified)
        {
            throw new InvalidOperationException("Your account already verified.");
        }

        var refreshToken = tokenService.CreateRefreshToken();
        var accessToken = tokenService.CreateAccessToken(storedUser);
        tokenService.SetRefreshToken(refreshToken);

        storedUser.IsVerified = true;
        storedUser.RefreshToken = refreshToken;

        userRepository.Update(WhisperMapper.Mapper.Map<UserEntity>(storedUser));
        await transactionManager.SaveChangesAsync();

        return new TokensModel
        {
            RefreshToken = refreshToken.Token,
            AccessToken = accessToken
        };
    }
}