using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Whisper.Core.Helpers;

namespace Whisper.Core.Registries;

public static class JwtDescriptorRegistry
{
    public static void AddJwtAuthentification(this IServiceCollection services, IConfiguration configuration)
    {
        string authTokenSigningKey = configuration.GetStringOrThrow("Token:HashKey");

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authTokenSigningKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}