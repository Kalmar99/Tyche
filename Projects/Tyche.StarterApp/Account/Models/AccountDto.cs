namespace Tyche.StarterApp.Account;

public class AccountDto
{
    public AccountDto(IReadOnlyCollection<User> users, string name, bool isCompanyAccount)
    {
        Users = users;
        Name = name;
        IsCompanyAccount = isCompanyAccount;
    }

    public IReadOnlyCollection<User> Users { get; }

    public string Name { get; }

    public bool IsCompanyAccount { get; }
}