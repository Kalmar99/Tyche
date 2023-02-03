using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Shared.CommonUserRepository;

public class CommonUserRepository : ICommonUserRepository
{
    private readonly IStorageClient<CommonUserStorageSettings> _storageClient;
    private readonly ILogger<CommonUserRepository> _logger;

    public CommonUserRepository(IStorageClient<CommonUserStorageSettings> storageClient, ILogger<CommonUserRepository> logger)
    {
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public async Task<User> GetByEmail(string email, CancellationToken ct = default)
    {
        try
        {
            var blobs = await _storageClient.Find(email, ct);
            var entityMetadata = blobs.Single();

            var userEntity = await _storageClient.Get<CommonUserEntity>(entityMetadata.Key, ct);

            return new User(userEntity.Key, userEntity.Name, userEntity.Email, userEntity.Password, userEntity.Role, userEntity.AccountId);
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, $"{nameof(CommonUserRepository)} found more than 1 user with the same email, this should not be possible");
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to get user by email");
            throw;
        }
    }
}