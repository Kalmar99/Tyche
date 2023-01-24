namespace Tyche.StarterApp.Shared;

public class AccountDto
{
    public AccountDto(IReadOnlyCollection<UserDto> users, string name, bool isCompanyAccount)
    {
        Users = users;
        Name = name;
        IsCompanyAccount = isCompanyAccount;
    }
    
    private IReadOnlyCollection<UserDto> Users { get; }

    public string Name { get; }

    public bool IsCompanyAccount { get; }
}