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

    public async Task<string> CreateAccount(AccountDto dto, string userName, string userEmail, string userPassword, CancellationToken ct = default)
    {
        var accountStorableEntity = AccountFactory.Create(dto, userEmail, userPassword);
        
        var adminUser = UserFactory.Create(userEmail, userName, userPassword, UserRole.AccountAdmin, accountStorableEntity.Id);
        
        accountStorableEntity.Users.Add(adminUser.Id);

        await _accountRepository.Set(accountStorableEntity, ct);

        await _userRepository.Set(adminUser, ct);

        return accountStorableEntity.Id;
    }

    public async Task AddUser (UserDto userDto, CancellationToken ct = default)
    {
        var account = await _accountRepository.Get(userDto.AccountId, ct);
        
        account.Users.Add(userDto.Id);

        await _userRepository.Set(userDto, ct);

        await _accountRepository.Set(account, ct);
    }

    public async Task DisableUser(string userId, CancellationToken ct = default)
    {
        var user = await _userRepository.Get(userId, ct);
        
        user.Disable();

        await _userRepository.Set(user, ct);
    }

}