using System;
using Tyche.StarterApp.Identity;

namespace Tyche.StarterApp.Tests.Identity;

internal static class IdentityStorableEntityTestFactory
{
    public static IdentityStorableEntity Create(string email)
    {
        var key = IdentityStorableEntity.GetKey(email);

        return new IdentityStorableEntity(key, email, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), IdentityRole.User);
    }
}