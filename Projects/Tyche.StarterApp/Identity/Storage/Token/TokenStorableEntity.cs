using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity.Token;

public class TokenStorableEntity : StorageEntity
{
    public TokenStorableEntity(string token, DateTime expires) : base(Guid.NewGuid().ToString())
    {
        Token = token;
        Expires = expires;
    }
    
    public string Token { get; }

    public DateTime Expires { get; }

    public bool IsInvalid(string baseString)
    {
        var token = Md5Hash.Generate(baseString);

        return TokenIsExpired() || TokenMismatch(token);
    }

    private bool TokenMismatch(string token) => !token.Equals(Token);

    private bool TokenIsExpired() => Expires < DateTime.UtcNow;

    public static TokenStorableEntity Create(string baseString) => new(Md5Hash.Generate(baseString), DateTime.UtcNow);
}