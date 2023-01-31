using System;
using System.Collections.Generic;
using Tyche.StarterApp.Account;

namespace Tyche.StarterApp.Tests.Account;

public static class AccountFactory
{
    public static StarterApp.Account.Account Create()
    {
        return new StarterApp.Account.Account(Guid.NewGuid().ToString(), new List<User>(), Guid.NewGuid().ToString(), false);
    }
    
    public static StarterApp.Account.Account CreateWithUser(User user)
    {
        return new StarterApp.Account.Account(Guid.NewGuid().ToString(), new List<User>() { user }, Guid.NewGuid().ToString(), false);
    }
}