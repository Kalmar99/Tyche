namespace Tyche.StarterApp.Shared.CommonUserRepository;

public interface ICommonUserRepository
{
    public Task<User> GetByEmail(string email, CancellationToken ct = default);
}