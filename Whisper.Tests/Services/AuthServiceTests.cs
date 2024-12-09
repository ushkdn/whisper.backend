using FakeItEasy;
using FluentAssertions;
using StackExchange.Redis;
using Whisper.Data.Dtos.User;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;
using Whisper.Services.AuthService;
using Whisper.Services.MessageService;

namespace Whisper.Tests.Services;

public class AuthServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionManager _transactionManager;
    private readonly ICacheRepository _cacheRepository;
    private readonly IMessageService _messageService;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
        _userRepository = A.Fake<IUserRepository>();
        _transactionManager = A.Fake<ITransactionManager>();
        _cacheRepository = A.Fake<ICacheRepository>();
        _messageService = A.Fake<IMessageService>();

        _authService = new AuthService(_userRepository,
            _transactionManager,
            _cacheRepository,
            _messageService
        );
    }

    [Fact]
    public async Task AuthService_Register_Ok()
    {
        //Arrange
        var userRegisterDto = A.Fake<UserRegisterDto>();
        //A.CallTo(async () => await userRepository.GetByEmailAsync(userRegisterDto.Email));
    }
}