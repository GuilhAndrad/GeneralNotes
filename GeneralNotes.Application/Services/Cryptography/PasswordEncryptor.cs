using System.Security.Cryptography;
using System.Text;

namespace GeneralNotes.Application.Services.Cryptography;
public class PasswordEncryptor(string additionalkey)
{
    private readonly string _additionalkey = additionalkey;

    public string Encrypt(string password)
    {
        var passwordWithAdditionalKey = $"{password}{_additionalkey}";

        var bytes = Encoding.UTF8.GetBytes(passwordWithAdditionalKey);
        byte[] hashBytes = SHA512.HashData(bytes);
        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
