using Microsoft.Extensions.DependencyInjection;
using Whisper.Services.MessageService;
using Whisper.Services.MessageService.EmailService;

namespace Whisper.Services.IoC.MessageService;

public static class MessageServiceContainer
{
    public delegate IMessageService MessageServiceResolver(MessageServiceType type);

    public static void RegisterResolvingMessageService(IServiceCollection services)
    {
        services.AddScoped<MessageServiceResolver>(serviceProvider => type =>
        {
            switch (type)
            {
                case MessageServiceType.Email:
                    return serviceProvider.GetRequiredService<EmailService>();

                default:
                    throw new KeyNotFoundException();
            }
        });
    }
}