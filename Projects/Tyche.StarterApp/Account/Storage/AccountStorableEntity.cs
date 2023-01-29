using Newtonsoft.Json;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class AccountStorableEntity : StorageEntity
{
    [JsonConstructor]
    public AccountStorableEntity(string id,List<string> users, string name, bool isCompanyAccount) 
        : base(id)
    {
        Users = users;
        Name = name;
        IsCompanyAccount = isCompanyAccount;
        Id = id;
    }

    public string Id { get; }
    
    public List<string> Users { get; }

    public string Name { get; }

    public bool IsCompanyAccount { get; }

    public static AccountStorableEntity Create(string id, string name, bool isCompanyAccount = true) =>
        new(id, new List<string>(), name, isCompanyAccount);
}