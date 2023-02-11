namespace Tyche.StarterApp.Identity;

public class RegisterDto
{
    public RegisterDto(string email, string password, string name, string accountName)
    {
        Email = email;
        Password = password;
        Name = name;
        AccountName = accountName;
    }

    public string Email { get; }

    public string Password { get; }

    public string Name { get; }

    public string AccountName { get; }

    public bool IsInvalid() => 
        string.IsNullOrEmpty(Email) ||
        string.IsNullOrEmpty(Password) ||
        string.IsNullOrEmpty(Name) ||
        string.IsNullOrEmpty(AccountName);
}