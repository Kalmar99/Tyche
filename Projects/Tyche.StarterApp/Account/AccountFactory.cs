namespace Tyche.StarterApp.Account;

internal static class AccountFactory
{
    public static AccountStorableEntity Create(AccountDto dto, string userEmail, string userPassword)
    {
        var accountId = Guid.NewGuid().ToString();

        return AccountStorableEntity.Create(accountId, dto.Name, dto.IsCompanyAccount);
    }
    
    public static AccountDto Create(AccountStorableEntity accountStorableEntity,
        IReadOnlyCollection<UserStorableEntity> userStorableEntities)
    {
        var users = userStorableEntities.Select(Map).Where(u => !u.Role.Equals(UserRole.Disabled)).ToList();
        
        return new AccountDto(users, accountStorableEntity.Name, accountStorableEntity.IsCompanyAccount);
    }
    
    private static UserDto Map(UserStorableEntity entity) => new(entity.Key, entity.Name, entity.Email, entity.Password, entity.Role, entity.AccountId);
}