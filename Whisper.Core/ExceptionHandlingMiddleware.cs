using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Whisper.Data.Extensions;
using Whisper.Data.Utils;

namespace Whisper.Core;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var serviceResponse = ex.ToServiceResponse<string>();

            context.Response.StatusCode = serviceResponse.StatusCode;

            var jsonResponse = JsonSerializer.Serialize(serviceResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
