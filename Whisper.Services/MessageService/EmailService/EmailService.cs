using MimeKit;
using MimeKit.Text;
using Whisper.Data.Extensions;
using Whisper.Data.Utils;
using MailKit.Net.Smtp;
using MailKit.Security;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data;

namespace Whisper.Services.MessageService.EmailService;

public class EmailService(ICacheRepository cacheRepository) : IEmailService
{
    public async Task<ServiceResponse<string>> SendMessage<T>(MessagePayload messagePayload)
    {
        //fetch from configuration
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse("conf string"));
            emailMessage.To.Add(MailboxAddress.Parse(messagePayload.User));
            emailMessage.Body = new TextPart(TextFormat.Plain)
            {
                Text = GenerateSecurityCode()
            };

            var smtp = new SmtpClient();

            smtp.Connect(
                "host",
                1234,
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate(
                "adminEmail",
                "adminPassword"
            );

            await smtp.SendAsync(emailMessage);
            smtp.Disconnect(true);

            await cacheRepository.SetStringAsync(CacheTables.SECRET_CODE, )

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