using Whisper.Data.Utils;

namespace Whisper.Services.MessageService;

public interface IMessageService
{
    Task<ServiceResponse<string>> SendMessage<T>(MessagePayload messagePayload);
}