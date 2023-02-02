namespace Tyche.StarterApp.Account;

internal class AccountOrchestrator : IAccountOrchestrator
{
    private readonly AccountService _accountService;
    private readonly AccountFactory _accountFactory;
    private readonly UserFactory _userFactory;

    public AccountOrchestrator(AccountService accountService, AccountFactory accountFactory, UserFactory userFactory)
    {
        _accountService = accountService;
        _accountFactory = accountFactory;
        _userFactory = userFactory;
    }
    
    public async Task<Account> Get(string accountId, CancellationToken ct = default)
    {
        return await _accountService.Get(accountId, ct);
    }

    public async Task<string> Create(AccountDto dto, UserDto userDto, CancellationToken ct = default)
    {
        var account = await _accountFactory.Create(dto, userDto, ct);

        await _accountService.Update(account, ct);

        return account.Id;
    }

    public async Task AttachUser (UserDto userDto, CancellationToken ct = default)
    {
        var account = await _accountService.Get(userDto.AccountId, ct);

        var user = await _userFactory.Create(userDto.Name, userDto.Email, userDto.Password, userDto.Role, userDto.AccountId, ct);
        
        account.AddUser(user);

        await _accountService.Update(account, ct);
    }

    public async Task DisableUser(string userId, string accountId, CancellationToken ct = default)
    {
        var account = await _accountService.Get(accountId, ct);
        
        account.DisableUser(userId);

        await _accountService.Update(account, ct);
    }

}