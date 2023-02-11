namespace Tyche.StarterApp.Identity;

public class RegisterDto
{
    public RegisterDto(string email, string password, string name)
    {
        Email = email;
        Password = password;
        Name = name;
    }

    public string Email { get; }

    public string Password { get; }

    public string Name { get; }

    public string AccountName { get; set; }
}