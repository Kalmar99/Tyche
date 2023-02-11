using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

public class SaltStorableEntity : StorageEntity
{
    public SaltStorableEntity(string key, byte[] salt) : base(key)
    {
        Salt = salt;
    }
    
    public byte[] Salt { get; }
}