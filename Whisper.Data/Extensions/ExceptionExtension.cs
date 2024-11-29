using Whisper.Data.Utils;
namespace Whisper.Data.Extensions;

public static class ExceptionExtension
{
    public static ServiceResponse<T> ToServiceResponse<T>(this Exception ex)
    {
        var serviceResponse = new ServiceResponse<T>();

        switch (ex)
        {
            case KeyNotFoundException _:
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 404;
                break;

            case InvalidOperationException _:
            case ArgumentNullException _:
            case ArgumentException _:
            case HttpRequestException _:
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 400;
                break;

            default:
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;
                break;
        }
        return serviceResponse;
    }
}