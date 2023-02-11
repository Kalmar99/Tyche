using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

internal static class UserFactory
{
    public static User Create(string name, string email, UserRole role, string accountId)
    {
        var userId = Md5Hash.Generate(email);

        return new User(userId, name, email, role, accountId);
    }
}