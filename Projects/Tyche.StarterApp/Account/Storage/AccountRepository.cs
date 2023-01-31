using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class AccountRepository
{
    private readonly IStorageClient<AccountStorageSettings> _storageClient;
    private readonly ILogger<AccountRepository> _logger;

    public AccountRepository(IStorageClient<AccountStorageSettings> storageClient, ILogger<AccountRepository> logger)
    {
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public virtual async Task Set(AccountStorableEntity entity, CancellationToken ct = default)
    {
        try
        {
            await _storageClient.Set(entity, ct);
        }
        catch (Exception exception)
        {
            var key = entity.Id;
            _logger.LogError(exception, "Failed to set AccountStorableEntity with key: {key}", key);
            throw;
        }
    }

    public virtual async Task<AccountStorableEntity> Get(string key, CancellationToken ct = default)
    {
        try
        {
            var entity = await _storageClient.Get<AccountStorableEntity>(key, ct);

            return entity;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get AccountStorableEntity with key: {key}", key);
            throw;
        }
    }
}