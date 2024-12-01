using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Whisper.Data.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using WhisperDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<WhisperDbContext>();

        dbContext.Database.Migrate();
    }
}