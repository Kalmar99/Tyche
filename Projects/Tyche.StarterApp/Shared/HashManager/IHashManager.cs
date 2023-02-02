namespace Tyche.StarterApp.Shared.HashManager;

public interface IHashManager
{
    public Task<string> GeneratePasswordHash(string password, string userId, CancellationToken ct = default);

    public Task<bool> VerifyPasswordHash(string password, string hash, string userId, CancellationToken ct = default);
}