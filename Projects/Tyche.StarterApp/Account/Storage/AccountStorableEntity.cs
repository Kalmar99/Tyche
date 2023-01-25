using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class AccountStorableEntity : StorageEntity
{
    public AccountStorableEntity(string id,IReadOnlyCollection<UserDto> users, string name, bool isCompanyAccount) 
        : base(id)
    {
        Users = users;
        Name = name;
        IsCompanyAccount = isCompanyAccount;
        Id = id;
    }

    public string Id { get; }
    
    public IReadOnlyCollection<UserDto> Users { get; }

    public string Name { get; }

    public bool IsCompanyAccount { get; }
}