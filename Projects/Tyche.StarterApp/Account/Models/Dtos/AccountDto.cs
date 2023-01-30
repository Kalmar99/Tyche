namespace Tyche.StarterApp.Account;

public class AccountDto
{
    public AccountDto(IReadOnlyCollection<UserDto> users, string name, bool isCompanyAccount)
    {
        Users = users;
        Name = name;
        IsCompanyAccount = isCompanyAccount;
    }

    public IReadOnlyCollection<UserDto> Users { get; }

    public string Name { get; }

    public bool IsCompanyAccount { get; }

    public bool IsInvalid() => string.IsNullOrEmpty(Name);
}