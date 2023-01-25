using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class AccountRepository : IAccountRepository
{
    private readonly IStorageClient<AccountStorageSettings> _storageClient;

    public AccountRepository(IStorageClient<AccountStorageSettings> storageClient)
    {
        _storageClient = storageClient;
    }
    
    public async Task Set(AccountDto dto, CancellationToken ct = default)
    {
        var id = Guid.NewGuid().ToString();
        
        var entity = MapToEntity(dto, id);
        
        await _storageClient.Set(entity, ct);
    }

    public async Task<AccountDto> Get(string key, CancellationToken ct = default)
    {
        var entity = await _storageClient.Get<AccountStorableEntity>(key, ct);

        var dto = MapToDto(entity);

        return dto;
    }

    private AccountStorableEntity MapToEntity(AccountDto dto, string id)
    {
        return new AccountStorableEntity(id, dto.Users, dto.Name, dto.IsCompanyAccount);
    }

    private AccountDto MapToDto(AccountStorableEntity entity)
    {
        return new AccountDto(entity.Users, entity.Name, entity.IsCompanyAccount);
    }
}