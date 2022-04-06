using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.EF.Configurations;

internal static class PasswordComputing
{
    internal static string GetHash(string password)
    {
        return Encoding.Unicode.GetString(SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(password)));
    }
}
