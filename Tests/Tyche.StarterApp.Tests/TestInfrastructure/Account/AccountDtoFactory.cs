using System.Collections.Generic;
using Tyche.StarterApp.Account;

namespace Tyche.StarterApp.Tests.Account;

internal static class AccountDtoFactory
{
    public static AccountDto Create(string name)
    {
        return new AccountDto(new List<UserDto>(), name, true);
    }
}