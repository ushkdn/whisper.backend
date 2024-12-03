using Whisper.Data.Utils;

namespace Whisper.Services.MessageService;

public interface IMessageService
{
    Task SendMessage(MessagePayload messagePayload);
}