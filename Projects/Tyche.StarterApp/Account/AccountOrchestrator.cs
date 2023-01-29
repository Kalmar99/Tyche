namespace Tyche.StarterApp.Account;

internal class AccountOrchestrator : IAccountOrchestrator
{
    private readonly AccountRepository _accountRepository;
    
    private readonly UserRepository _userRepository;

    public AccountOrchestrator(AccountRepository accountRepository, UserRepository userRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }
    
    public async Task<AccountDto> GetAccount(string accountId, CancellationToken ct = default)
    {
        var accountStorableEntity = await _accountRepository.Get(accountId, ct);

        var userStorableEntities = await _userRepository.Get(accountStorableEntity.Users, ct);

        var account = AccountFactory.Create(accountStorableEntity, userStorableEntities);

        return account;
    }

    public async Task CreateAccount(AccountDto dto, string userEmail, string userPassword, CancellationToken ct = default)
    {
        var accountStorableEntity = AccountFactory.Create(dto, userEmail, userPassword);
        
        var adminUser = UserFactory.Create(userEmail, userPassword, UserRole.AccountAdmin, accountStorableEntity.Id);
        
        accountStorableEntity.Users.Add(adminUser.Id);

        await _accountRepository.Set(accountStorableEntity, ct);

        await _userRepository.Set(adminUser, ct);
    }

    public async Task AddUser (User user, CancellationToken ct = default)
    {
        var account = await _accountRepository.Get(user.AccountId, ct);
        
        account.Users.Add(user.Id);

        await _userRepository.Set(user, ct);

        await _accountRepository.Set(account, ct);
    }

    public async Task DisableUser(User user, CancellationToken ct = default)
    {
        user.Disable();

        await _userRepository.Set(user, ct);
    }

}