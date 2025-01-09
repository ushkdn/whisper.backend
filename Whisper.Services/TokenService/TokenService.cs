using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Whisper.Core.Helpers;
using Whisper.Data.Entities;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Repositories.RefreshTokenRepository;

namespace Whisper.Services.TokenService;

public class TokenService(
    IConfiguration configuration,
    IHttpContextAccessor httpContext,
    IRefreshTokenRepository refreshTokenRepository
    ) : ITokenService
{
    private readonly string tokenHashKey = configuration.GetStringOrThrow("Token:HashKey");

    public async Task<AuthTokensModel> RefreshTokens()
    {
        var refreshTokenFromCookie = httpContext.HttpContext.Request.Cookies["refresh-token"]
            ?? throw new ArgumentException("Missing refresh-token. Please log-in again.");

        var storedRefreshToken = WhisperMapper.Mapper.Map<RefreshTokenModel>(
            await refreshTokenRepository.GetRelatedByTokenAsync(refreshTokenFromCookie)
        );

        if (storedRefreshToken.ExpireDate < DateTime.UtcNow)
        {
            httpContext.HttpContext.Response.Cookies.Delete("refresh-token");
            throw new SecurityTokenExpiredException("Refresh token expired. Please log-in.");
        }

        var authTokens = CreateTokensAndSetRefreshToken(storedRefreshToken.User);

        refreshTokenRepository.Update(WhisperMapper.Mapper.Map<RefreshTokenEntity>(authTokens.RefreshToken));

        return new AuthTokensModel(authTokens.AccessToken, authTokens.RefreshToken);
    }

    public RefreshTokenModel CreateRefreshToken()
    {
        return new RefreshTokenModel
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddDays(7)
        };
    }

    public void SetRefreshToken(RefreshTokenModel refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.ExpireDate
        };

        httpContext.HttpContext.Response.Cookies.Append("refresh-token", refreshToken.Token, cookieOptions);
    }

    public AuthTokensModel CreateTokensAndSetRefreshToken(UserModel user)
    {
        var refreshToken = CreateRefreshToken();
        var accessToken = CreateAccessToken(user);
        SetRefreshToken(refreshToken);

        return new AuthTokensModel(accessToken, refreshToken);
    }

    public string CreateAccessToken(UserModel userModel)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("Id", $"{userModel.Id}"),
            new Claim(ClaimTypes.Email, userModel.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenHashKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return accessToken;
    }
}