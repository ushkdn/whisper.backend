using Whisper.Data;
using Whisper.Services.UserService;

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


        builder.Services.AddScoped<IUserService, UserService>();

        new DependencyContainerConfiguration(builder.Services, builder.Configuration)
            .RegisterServices()
            .RegisterDatabase("Postgres")
            .SetNpgsqlContext();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}