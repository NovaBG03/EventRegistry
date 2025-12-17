namespace EventRegistry.Business.Security.Contracts;

public interface ISecretHasher
{
    string Hash(string secret);
    bool Validate(string secret, string hash);
}
