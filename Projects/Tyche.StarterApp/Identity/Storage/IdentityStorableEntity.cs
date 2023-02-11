using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

internal class IdentityStorableEntity : StorageEntity
{
    public IdentityStorableEntity(string key, string email, string password, string name, IdentityRole role) : base(key, null)
    {
        Email = email;
        Password = password;
        Name = name;
        Role = role;
    }
    
    public string Email { get; }

    public string Password { get; }

    public string Name { get; }

    public IdentityRole Role { get; }
}