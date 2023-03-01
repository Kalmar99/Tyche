using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity.Token;

public class InvitationRepository
{
    private readonly IStorageClient<InvitationStorageSettings> _storageClient;
    private readonly ILogger<InvitationRepository> _logger;

    public InvitationRepository(IStorageClient<InvitationStorageSettings> storageClient, ILogger<InvitationRepository> logger)
    {
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public async Task<InvitationStorableEntity> Get(string key, CancellationToken ct = default)
    {
        try
        {
            var entity = await _storageClient.Get<InvitationStorableEntity>(key, ct);

            return entity;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get invitation with key: {key}", key);
            throw;
        }
    }
    
    public virtual async Task Set(InvitationStorableEntity entity, CancellationToken ct = default)
    {
        try
        {
            await _storageClient.Set(entity, ct);
        }
        catch (Exception exception)
        {
            var key = entity.Key;
            _logger.LogError(exception, "Failed to set invitation with key: {key}", key);
            throw;
        }
    }

    public virtual async Task Delete(string key, CancellationToken ct = default)
    {
        try
        {
            await _storageClient.Delete(key, ct);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed delete invitation with key: {key}", key);
            throw;
        }
    }
}