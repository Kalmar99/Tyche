namespace Tyche.StarterApp.Account;

public sealed record AddAccountRequestDto
{
    public AddAccountRequestDto(AccountDto account, UserDto user)
    {
        Account = account;
        User = user;
    }
    
    public AccountDto? Account { get; }

    public UserDto? User { get; }

    public bool IsInvalid() => (User?.IsInvalid() ?? false) || (Account?.IsInvalid() ?? false);
}