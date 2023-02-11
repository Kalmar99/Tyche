using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

internal class IdentityRepository
{
    private readonly IStorageClient<IdentityStorageSettings> _storageClient;
    private readonly ILogger<IdentityRepository> _logger;

    public IdentityRepository(IStorageClient<IdentityStorageSettings> storageClient, ILogger<IdentityRepository> logger)
    {
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public virtual async Task Set(IdentityStorableEntity entity, CancellationToken ct = default)
    {
        var key = Md5Hash.Generate(entity.Email);
        
        try
        {
            await _storageClient.Set(entity, ct);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to set user with key: {key}", key);
            throw;
        }
    }
    
    public async Task<IdentityStorableEntity> Get(string email, CancellationToken ct = default)
    {
        var key = Md5Hash.Generate(email);
        
        try
        {
            var entity = await _storageClient.Get<IdentityStorableEntity>(key, ct);

            return entity;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get user with key: {key}", key);
            throw;
        }
    }
}