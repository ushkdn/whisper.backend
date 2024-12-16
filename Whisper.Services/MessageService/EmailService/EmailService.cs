using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Whisper.Core.Helpers;
using Whisper.Data;
using Whisper.Data.CacheModels;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Utils;

namespace Whisper.Services.MessageService.EmailService;

public class EmailService(IConfiguration configuration) : IEmailService
{
    private readonly int whisperMessagingEmailPort = int.Parse(configuration.GetStringOrThrow("Messaging:Email:Port"));
    private readonly string whisperMessagingEmailEmail = configuration.GetStringOrThrow("Messaging:Email:Email");
    private readonly string whisperMessagingEmailHost = configuration.GetStringOrThrow("Messaging:Email:Host");
    private readonly string whisperMessagingEmailPassword = configuration.GetStringOrThrow("Messaging:Email:Password");

    public async Task SendMessage(MessagePayload messagePayload)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(MailboxAddress.Parse(whisperMessagingEmailEmail));
        emailMessage.To.Add(MailboxAddress.Parse(messagePayload.UserEmail));
        emailMessage.Body = new TextPart(TextFormat.Plain)
        {
            Text = messagePayload.Message,
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
    }
}