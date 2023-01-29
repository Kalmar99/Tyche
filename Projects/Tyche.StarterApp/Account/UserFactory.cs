using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

public static class UserFactory
{
    public static User Create(string email, string password, UserRole role, string accountId)
    {
        var userId = Md5Hash.Generate(email);
        return new User(userId, email, password, role, accountId);
    }
}