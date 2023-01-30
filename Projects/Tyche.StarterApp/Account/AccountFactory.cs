namespace Tyche.StarterApp.Account;

internal static class AccountFactory
{
    public static Account Create(AccountDto accountDto, UserDto userDto)
    {
        var accountId = Guid.NewGuid().ToString();
        
        var user = new User(userDto.Name, userDto.Email, userDto.Password, UserRole.AccountAdmin, accountId);
        var account = new Account(accountId, new List<User>() { user }, accountDto.Name, accountDto.IsCompanyAccount);
        return account;
    }
}