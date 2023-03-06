namespace Tyche.StarterApp.Identity;

internal class HashManager
{
    private readonly SaltRepository _repository;

    public HashManager(SaltRepository repository)
    {
        _repository = repository;
    }
    
    public virtual async Task<string> GeneratePasswordHash(string password, string userId, CancellationToken ct = default)
    {
        var hashedPassword = PasswordHasher.GenerateHash(password, out byte[] salt);

        var saltEntity = new SaltStorableEntity(userId, salt);

        await _repository.Set(saltEntity, ct);

        return hashedPassword;
    }

    public virtual async Task<bool> VerifyPasswordHash(string password, string hash, string userId, CancellationToken ct = default)
    {
        var saltEntity = await _repository.Get(userId, ct);

        return PasswordHasher.VerifyHash(password, hash, saltEntity.Salt);
    }
}