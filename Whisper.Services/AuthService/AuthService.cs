using Whisper.Data;
using Whisper.Data.CacheModels;
using Whisper.Data.Entities;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.RefreshTokenRepository;
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
    IMessageService messageService,
    ITokenService tokenService,
    IRefreshTokenRepository refreshTokenRepository
    ) : IAuthService
{
    public async Task ForgotPassword(UserModel user)
    {
    }

    public async Task<TokensModel> LogIn(UserModel user)
    {
        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetByEmailAsync(user.Email))
            ?? throw new ArgumentException("Wrong email or password.");

        if (!BCrypt.Net.BCrypt.Verify(user.Password, storedUser?.Password))
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

        await messageService.SendMessage(new MessagePayload { UserEmail = user.Email, });
    }

    public async Task ResetPassword(UserModel user)
    {
    }

    public async Task<TokensModel> Verify(UserModel user)
    {
        var cachedSecretCode = await cacheRepository.GetSingleAsync<CacheSecretCode>(
            CacheTables.SECRET_CODE + $":{user.Email}"
        );

        if (cachedSecretCode is null)
        {
            throw new ArgumentNullException("Your secret code expires. Please try again.");
        }

        if (user.SecretCode != cachedSecretCode.SecretCode)
        {
            throw new ArgumentException("Wrong email or secret code.");
        }

        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetByEmailAsync(user.Email));

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