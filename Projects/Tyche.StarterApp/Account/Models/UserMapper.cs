using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

internal static class UserMapper
{
    public static UserStorableEntity Map(User user)
    {
        var userId = Md5Hash.Generate(user.Email);
        return new UserStorableEntity(userId, user.Name, user.Email, user.Role, user.AccountId);
    }
    
    public static User Map(UserStorableEntity entity)
    {
        return new User(entity.Key, entity.Name, entity.Email, entity.Role, entity.AccountId);
    }
}