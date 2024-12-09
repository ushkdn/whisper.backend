using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Whisper.Core.Registries;

public class DotEnvRegistry(IHostEnvironment _env)
{
    public DotEnvRegistry AddDotEnvConfiguration(IConfigurationBuilder _configBuilder)
    {
        var name = _env.EnvironmentName;
        _configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{name}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();
        return this;
    }
}