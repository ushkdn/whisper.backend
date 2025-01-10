using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Dtos.Tokens;
using Whisper.Data.Extensions;
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
        var serviceResponse = new ServiceResponse<GetAuthTokensDto>();

        try
        {
            var authTokens = await tokenService.RefreshTokens();

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Your account has been verified.";
            serviceResponse.Data = new GetAuthTokensDto(authTokens.AccessToken, authTokens.RefreshToken.Token);
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<GetAuthTokensDto>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse);
    }
}