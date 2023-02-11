using System;
using Tyche.StarterApp.Account;

namespace Tyche.StarterApp.Tests.Account;

public static class UserDtoFactory
{
    public static UserDto Create()
    {
        return new UserDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "test@example.com", UserRole.User, Guid.NewGuid().ToString());
    }
}