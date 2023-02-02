using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Shared.HashManager;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Shared.SecureHasher;

public class SaltRepository
{
    private readonly IStorageClient<SaltStorageSettings> _storageClient;
    private readonly ILogger<SaltRepository> _logger;

    public SaltRepository(IStorageClient<SaltStorageSettings> storageClient, ILogger<SaltRepository> logger)
    {
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public async Task<SaltStorableEntity> Get(string key, CancellationToken ct = default)
    {
        try
        {
            var entity = await _storageClient.Get<SaltStorableEntity>(key, ct);

            return entity;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get salt with key: {key}", key);
            throw;
        }
    }
    
    public virtual async Task Set(SaltStorableEntity entity, CancellationToken ct = default)
    {
        try
        {
            await _storageClient.Set(entity, ct);
        }
        catch (Exception exception)
        {
            var key = entity.Key;
            _logger.LogError(exception, "Failed to set salt with key: {key}", key);
            throw;
        }
    }
}