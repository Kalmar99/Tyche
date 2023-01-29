using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

public static class UserFactory
{
    public static UserDto Create(string email, string name, string password, UserRole role, string accountId)
    {
        var userId = Md5Hash.Generate(email);
        return new UserDto(userId, name, email, password, role, accountId);
    }
}