using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity.Token;

public class TokenRepository
{
    private readonly IStorageClient<TokenStorageSettings> _storageClient;
    private readonly ILogger<TokenRepository> _logger;

    public TokenRepository(IStorageClient<TokenStorageSettings> storageClient, ILogger<TokenRepository> logger)
    {
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public async Task<TokenStorableEntity> Get(string key, CancellationToken ct = default)
    {
        try
        {
            var entity = await _storageClient.Get<TokenStorableEntity>(key, ct);

            return entity;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get salt with key: {key}", key);
            throw;
        }
    }
    
    public virtual async Task Set(TokenStorableEntity entity, CancellationToken ct = default)
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