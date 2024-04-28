namespace FullPotential.Management.Utilities;

public interface ICryptoService
{
    byte[] GetNewSalt();

    byte[] Pbkdf2(string password, byte[] salt);

    string GetNewToken();
}
