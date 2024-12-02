using Whisper.Data;
using Whisper.Services.UserService;
using Whisper.Data.Extensions;
using Whisper.Core.Registies;

namespace Whisper.User;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();

        _ = new DotEnvRegistry(builder.Environment)
            .AddDotEnvConfiguration(builder.Configuration);

        new DependencyContainerConfiguration(builder.Services, builder.Configuration)
            .RegisterServices()
            .RegisterDatabase("Postgres")
            .RegisterCacheStorage("Redis")
            .SetNpgsqlContext();

        builder.Services.AddScoped<IUserService, UserService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.ApplyMigrations();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.MapControllers();

        app.Run();
    }
}