using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class UserRepository
{
    private readonly IStorageClient<UserStorageSettings> _storageClient;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(IStorageClient<UserStorageSettings> storageClient, ILogger<UserRepository> logger)
    {
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public async Task Set(UserDto userDto, CancellationToken ct = default)
    {
        var key = Md5Hash.Generate(userDto.Email);
        
        try
        {
            var entity = MapToEntity(userDto, key);
            await _storageClient.Set(entity, ct);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to set user with key: {key}", key);
            throw;
        }
    }
    
    public async Task Set(UserStorableEntity entity, CancellationToken ct = default)
    {
        try
        {
            await _storageClient.Set(entity, ct);
        }
        catch (Exception exception)
        {
            var key = entity.Key;
            _logger.LogError(exception, "Failed to set user with key: {key}", key);
            throw;
        }
    }

    public async Task<UserStorableEntity> Get(string key, CancellationToken ct = default)
    {
        try
        {
            var entity = await _storageClient.Get<UserStorableEntity>(key, ct);

            return entity;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get user with key: {key}", key);
            throw;
        }
    }

    public async Task<IReadOnlyCollection<UserStorableEntity>> Get(IReadOnlyCollection<string> keys, CancellationToken ct = default)
    {
        try
        {
            var userStorableEntityList = new List<UserStorableEntity>();

            foreach (var userKey in keys)
            {
                var userStorableEntity = await Get(userKey, ct);
                userStorableEntityList.Add(userStorableEntity);
            }

            return userStorableEntityList;
        }
        catch (Exception exception)
        {
            var stringKeys = string.Join(",", keys);
            _logger.LogError(exception, "Failed to get users with keys: {stringKeys}", stringKeys);
            throw;
        }
    }

    private UserStorableEntity MapToEntity(UserDto userDto, string key)
    {
        return new UserStorableEntity(key, userDto.Name, userDto.Email, userDto.Password, userDto.Role, userDto.AccountId);
    }
}