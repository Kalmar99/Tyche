using System;
using Tyche.StarterApp.Identity;

namespace Tyche.StarterApp.Tests.Identity;

public static class InvitationStorableEntityFactory
{
    public static InvitationStorableEntity Create()
    {
        return new InvitationStorableEntity(string.Empty, DateTime.UtcNow.AddDays(2));
    }
}