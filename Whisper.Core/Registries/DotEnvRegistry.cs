using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Whisper.Core.Registries;

public class DotEnvRegistry(IHostEnvironment env)
{
    public DotEnvRegistry AddDotEnvConfiguration(IConfigurationBuilder configBuilder)
    {
        var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\.env"));

        Env.Load(path);
        var name = env.EnvironmentName;
        configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{name}.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();
        return this;
    }
}