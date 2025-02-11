using Whisper.Core;
using Whisper.Core.Registries;
using Whisper.Data;
using Whisper.Data.Extensions;
using Whisper.Services.AuthService;
using Whisper.Services.IoC.MessageService;
using Whisper.Services.MessageService.EmailService;
using Whisper.Services.TokenService;

namespace Whisper.Auth;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        _ = new DotEnvRegistry(builder.Environment)
            .AddDotEnvConfiguration(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureSwagger();

        builder.Services.AddOpenApi();

        builder.Services.AddJwtAuthentification(builder.Configuration);

        DataDependencyContainerConfiguration.SetNpgsqlContext();

        builder.Services
            .RegisterServices()
            .RegisterDatabase(builder.Configuration, "Postgres")
            .RegisterCacheStorage(builder.Configuration, "Redis")
            .RegisterValidators();

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<EmailService>();

        //todo: mb rewrite architecture?
        MessageServiceContainer.RegisterResolvingMessageService(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.ApplyMigrations();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}