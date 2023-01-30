using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

public class Account
{
    public Account(string id, List<User> users, string name, bool isCompanyAccount)
    {
        Id = id;
        Users = users;
        Name = name;
        IsCompanyAccount = isCompanyAccount;
    }
    
    public string Id { get; }
    
    public List<User> Users { get; }
    
    public string Name { get; }

    public bool IsCompanyAccount { get; }

    public void AddUser(string name, string email, string password)
    {
        var user = new User(name, email, password, UserRole.User, Id);
        
        Users.Add(user);
    }

    public void DisableUser(string userId)
    {
        Users.FirstOrDefault(u => u.Id.Equals(userId))?.Disable();
    }
}