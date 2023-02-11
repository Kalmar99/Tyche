using Newtonsoft.Json;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class AccountStorableEntity : StorageEntity
{
    [JsonConstructor]
    public AccountStorableEntity(string id,List<string> users, string name) 
        : base(id)
    {
        Users = users;
        Name = name;
        Id = id;
    }

    public string Id { get; }
    
    public List<string> Users { get; }

    public string Name { get; }
}