using Whisper.Data;
using Whisper.Services.UserService;

namespace Whisper;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();


        builder.Services.AddSingleton<IUserService, UserService>();
            
        new DependencyContainerConfiguration(builder.Services, builder.Configuration)
            .RegisterServices()
            .RegisterDatabase("Postgres");

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}