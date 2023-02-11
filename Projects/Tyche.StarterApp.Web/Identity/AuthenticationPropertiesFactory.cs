using Microsoft.AspNetCore.Authentication;

namespace Tyche.StarterApp.Identity;

public static class AuthenticationPropertiesFactory
{
    public static AuthenticationProperties Create()
    {
        return new AuthenticationProperties
        {
            IsPersistent = true,
            RedirectUri = null,
            IssuedUtc = DateTime.UtcNow,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            AllowRefresh = true
        };
    }
}