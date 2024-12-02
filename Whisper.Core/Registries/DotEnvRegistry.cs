using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Whisper.Core.Registries;

public class DotEnvRegistry(IHostEnvironment env)
{
    public DotEnvRegistry AddDotEnvConfiguration(IConfigurationBuilder configBuilder)
    {
        var name = env.EnvironmentName;
        configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{name}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();
        return this;
    }
}