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

namespace Whisper.Services.AuthService;

public class AuthService(
    IUserRepository userRepository,
    ITransactionManager transactionManager,
    ICacheRepository cacheRepository,
    IMessageService messageService) : IAuthService
{
    public async Task ForgotPassword(UserModel user)
    {
    }

    public async Task LogIn(UserModel user)
    {
        var storedUser = await userRepository.GetByEmailAsync(user.Email)
            ?? throw new ArgumentException("Wrong email or password.");

        if (!BCrypt.Net.BCrypt.Verify(user.Password, storedUser?.Password))
        {
            throw new ArgumentException("Wrong email/phonenumber or password.");
        }

        if (!storedUser.IsVerified)
        {
            throw new InvalidOperationException("Your account is not verified.");
        }

        //token zone should be here
    }

    public async Task Register(UserModel user)
    {
        //remove all not verified accs via quartz every 1h??
        var storedUser = await userRepository.GetByEmailAsync(user.Email);
        if (storedUser is not null)
        {
            throw new InvalidOperationException("User already exists.");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        await userRepository.CreateAsync(WhisperMapper.Mapper.Map<UserEntity>(user));
        await transactionManager.SaveChangesAsync();

        await messageService.SendMessage(new MessagePayload { UserEmail = user.Email, });
        //mail send here
    }

    public async Task ResetPassword(UserModel user)
    {
    }

    public async Task Verify(UserModel user)
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

        storedUser.IsVerified = true;
        userRepository.Update(WhisperMapper.Mapper.Map<UserEntity>(storedUser));
        await transactionManager.SaveChangesAsync();
    }
}