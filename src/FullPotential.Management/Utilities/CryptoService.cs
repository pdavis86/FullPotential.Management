namespace FullPotential.Management.Utilities;

using System.Security.Cryptography;

public class CryptoService : ICryptoService
{
    public byte[] GetNewSalt()
    {
        return RandomNumberGenerator.GetBytes(32);
    }

    public byte[] Pbkdf2(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(password, salt, 600_000, HashAlgorithmName.SHA256, 256);
    }

    public string GetNewToken()
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcedefghijklmnopqrstuvwxyz1234567890";
        return RandomNumberGenerator.GetString(alphabet, 32);
    }
}
