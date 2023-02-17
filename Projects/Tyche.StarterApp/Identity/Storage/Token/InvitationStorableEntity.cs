using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity.Token;

public class InvitationStorableEntity : StorageEntity
{
    public InvitationStorableEntity(string value, DateTime expires) : base(Guid.NewGuid().ToString())
    {
        Value = value;
        Expires = expires;
    }
    
    public string Value { get; }

    public DateTime Expires { get; }

    public bool IsInvalid()
    {
        return TokenIsExpired();
    }

    private bool TokenIsExpired() => Expires < DateTime.UtcNow;

    public static InvitationStorableEntity Create(string baseString) => new(Md5Hash.Generate(baseString), DateTime.UtcNow);
}