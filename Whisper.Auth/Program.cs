using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
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
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Whisper.Auth API",
                Version = "v1",
                Description = "An API to perform auth operations",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Ushkan Daniil",
                    Email = "ushkndn@gmal.com",
                    Url = new Uri("https://github.com/ushkdn"),
                }
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standart authorization header using the Bearer Scheme (\"bearer [space] {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        builder.Services.AddOpenApi();

        builder.Services.AddJwtAuthentification(builder.Configuration);

        DataDependencyContainerConfiguration.RegisterServices(builder.Services);
        DataDependencyContainerConfiguration.RegisterDatabase(builder.Services, builder.Configuration, "Postgres");
        DataDependencyContainerConfiguration.RegisterCacheStorage(builder.Services, builder.Configuration, "Redis");
        DataDependencyContainerConfiguration.RegisterValidators(builder.Services);
        DataDependencyContainerConfiguration.SetNpgsqlContext();

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