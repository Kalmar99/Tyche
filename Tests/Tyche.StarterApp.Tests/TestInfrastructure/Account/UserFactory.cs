using System;
using Tyche.StarterApp.Account;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Tests.Account;

public static class UserFactory
{
    public static User Create()
    {
        return new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), UserRole.AccountAdmin, Guid.NewGuid().ToString());
    }
}