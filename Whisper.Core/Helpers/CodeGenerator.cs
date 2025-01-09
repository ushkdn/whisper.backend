namespace Whisper.Core.Helpers;

public static class CodeGenerator
{
    public static string GenerateSecurityCode()
    {
        var rnd = new Random();

        const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] result = new char[8];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = CHARS[rnd.Next(CHARS.Length)];
        }
        return new string(result);
    }
}