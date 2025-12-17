using System.Security.Cryptography;
using System.Text;
using EventRegistry.Business.Security.Contracts;

namespace EventRegistry.Business.Security;

public class Sha256SecretHasher : ISecretHasher
{
    public string Hash(string secret)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(secret));
        return Convert.ToHexString(bytes);
    }

    public bool Validate(string secret, string hash)
    {
        var computedHash = Hash(secret);
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(computedHash),
            Encoding.UTF8.GetBytes(hash));
    }
}
