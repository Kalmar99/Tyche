using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

public class InvitationStorableEntity : StorageEntity
{
    public InvitationStorableEntity(string accountId, DateTime expires) : base(Guid.NewGuid().ToString())
    {
        AccountId = accountId;
        Expires = expires;
    }

    public string AccountId { get; }
    
    public DateTime Expires { get; }

    public bool IsInvalid()
    {
        return InvitationIsExpired();
    }

    private bool InvitationIsExpired() => Expires < DateTime.UtcNow;

    public static InvitationStorableEntity Create(string accountId, DateTime inviteExpires) => new( accountId, inviteExpires );
}