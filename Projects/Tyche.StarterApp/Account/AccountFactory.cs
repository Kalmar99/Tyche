using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

internal class AccountFactory
{
    private readonly UserFactory _userFactory;

    public AccountFactory(UserFactory userFactory)
    {
        _userFactory = userFactory;
    }
    
    public async Task<Account> Create(AccountDto accountDto, UserDto userDto, CancellationToken ct = default)
    {
        var accountId = Guid.NewGuid().ToString();
        
        var user = await _userFactory.Create(userDto.Name, userDto.Email, userDto.Password, UserRole.AccountAdmin, accountId, ct);
        
        var account = new Account(accountId, new List<User>() { user }, accountDto.Name, accountDto.IsCompanyAccount);
        
        return account;
    }
}