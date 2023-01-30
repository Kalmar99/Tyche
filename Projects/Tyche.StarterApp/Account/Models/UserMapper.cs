using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

internal static class UserMapper
{
    public static UserStorableEntity Map(User user)
    {
        var userId = Md5Hash.Generate(user.Email);
        return new UserStorableEntity(userId, user.Name, user.Email, user.Password, user.Role, user.AccountId);
    }
    
    public static User Map(UserStorableEntity entity)
    {
        return new User(entity.Name, entity.Email, entity.Password, entity.Role, entity.AccountId);
    }
}