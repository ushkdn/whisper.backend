using FakeItEasy;
using Whisper.Data.Dtos.User;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;
using Whisper.Services.AuthService;
using Whisper.Services.MessageService;

namespace Whisper.Tests.Services;

public class AuthServiceTests
{
    private readonly IUserRepository userRepository;
    private readonly ITransactionManager transactionManager;
    private readonly ICacheRepository cacheRepository;
    private readonly IMessageService messageService;
    private readonly IAuthService authService;

    public AuthServiceTests()
    {
        userRepository = A.Fake<IUserRepository>();
        transactionManager = A.Fake<ITransactionManager>();
        cacheRepository = A.Fake<ICacheRepository>();
        messageService = A.Fake<IMessageService>();

        authService = new AuthService(userRepository,
            transactionManager,
            cacheRepository,
            messageService
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