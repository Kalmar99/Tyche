using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

internal static class AccountFactory
{
    public static Account Create(AccountDto accountDto, UserDto userDto)
    {
        var accountId = Guid.NewGuid().ToString();
        
        var user = UserFactory.Create(userDto.Name, userDto.Email, UserRole.AccountAdmin, accountId);
        
        var account = new Account(accountId, new List<User>() { user }, accountDto.Name);
        
        return account;
    }
    
    public static Account Create(string accountName, string userName, string userEmail)
    {
        var accountId = Guid.NewGuid().ToString();
        
        var user = UserFactory.Create(userName, userEmail, UserRole.AccountAdmin, accountId);
        
        var account = new Account(accountId, new List<User>() { user }, accountName);
        
        return account;
    }
}