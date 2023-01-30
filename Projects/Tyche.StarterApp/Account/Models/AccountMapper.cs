
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

internal static class AccountMapper
{
    public static Account Map(AccountStorableEntity accountStorableEntity, IReadOnlyCollection<UserStorableEntity> userStorableEntities)
    {
        var users = userStorableEntities.Select(UserMapper.Map).ToList();

        return new Account(accountStorableEntity.Id, users, accountStorableEntity.Name, accountStorableEntity.IsCompanyAccount);
    }

    public static AccountStorableEntity Map(Account account)
    {
        var userIds = account.Users.Select(u => Md5Hash.Generate(u.Email)).ToList();
        
        return new AccountStorableEntity(account.Id, userIds, account.Name, account.IsCompanyAccount);
    }
}