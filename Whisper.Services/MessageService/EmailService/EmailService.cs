using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

using MimeKit;
using MimeKit.Text;
using Whisper.Core.Helpers;
using Whisper.Data.Extensions;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Utils;

namespace Whisper.Services.MessageService.EmailService;

public class EmailService(ICacheRepository cacheRepository, IConfiguration configuration) : IEmailService
{
    private readonly string whisperMessagingEmailEmail = configuration.GetStringOrThrow("Messaging:Email:Email");
    private readonly string whisperMessagingEmailHost = configuration.GetStringOrThrow("Messaging:Email:Host");
    private readonly string whisperMessagingEmailPassword = configuration.GetStringOrThrow("Messaging:Email:Password");
    private readonly int whisperMessagingEmailPort = int.Parse(configuration.GetStringOrThrow("Messaging:Email:Port"));

    public async Task<ServiceResponse<string>> SendMessage<T>(MessagePayload messagePayload)
    {
        //fetch from configuration
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(whisperMessagingEmailEmail));
            emailMessage.To.Add(MailboxAddress.Parse(messagePayload.User));
            emailMessage.Body = new TextPart(TextFormat.Plain)
            {
                Text = GenerateSecurityCode()
            };

            var smtp = new SmtpClient();

            smtp.Connect(
                whisperMessagingEmailHost,
                whisperMessagingEmailPort,
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate(
                whisperMessagingEmailEmail,
                whisperMessagingEmailPassword
            );

            await smtp.SendAsync(emailMessage);
            smtp.Disconnect(true);

            //await cacheRepository.SetStringAsync(CacheTables.SECRET_CODE, )

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Secret code for account verification sent to your email.";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }
        return serviceResponse;
    }

    private static string GenerateSecurityCode()
    {
        var rnd = new Random();

        const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] result = new char[8];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = CHARS[rnd.Next(CHARS.Length)];
        }
        return new string(result);
    }
}