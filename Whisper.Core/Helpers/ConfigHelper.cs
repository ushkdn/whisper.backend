using Microsoft.Extensions.Configuration;

namespace Whisper.Core.Helpers;

public static class ConfigHelper
{
    public static string GetStringOrThrow(this IConfiguration config, string key)
    {
        var result = string.IsNullOrWhiteSpace(config[key])
            ? throw new ArgumentException("lol")
            : config[key]!;

#if DEBUG
        Console.WriteLine($"Got config from {key}:{result}");
#endif

        return result;
    }
}