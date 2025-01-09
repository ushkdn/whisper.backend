using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Extensions;
using Whisper.Data.Models;
using Whisper.Data.Utils;
using Whisper.Services.TokenService;

namespace Whisper.Auth.Controllers;

[Route("api/tokens")]
[ApiController]
public class TokenController(ITokenService tokenService) : ControllerBase
{
    [HttpPost("refresh-tokens")]
    public async Task<IActionResult> RefreshTokens()
    {
        var serviceResponse = new ServiceResponse<AuthTokensModel>();

        try
        {
            var tokens = await tokenService.RefreshTokens();

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Your account has been verified.";
            serviceResponse.Data = tokens;
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<AuthTokensModel>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Data);
    }
}