using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Whisper.Core.Helpers;
using Whisper.Data.Extensions;
using Whisper.Data.Models;
using Whisper.Data.Utils;

namespace Whisper.Services.TokenService;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly string tokenHashKey = configuration.GetStringOrThrow("Token:HashKey");

    public async Task RefreshToken()
    {
    }

    private RefreshTokenModel CreateRefreshToken()
    {
        return new RefreshTokenModel
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };
    }

    private string CreateAccessToken(UserModel userModel)
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

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}