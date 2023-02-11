using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

internal class Account
{
    public Account(string id, List<User> users, string name)
    {
        Id = id;
        Users = users;
        Name = name;
    }
    
    public string Id { get; }
    
    public List<User> Users { get; }
    
    public string Name { get; }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void DisableUser(string userId)
    {
        Users.FirstOrDefault(u => u.Id.Equals(userId))?.Disable();
    }
}