namespace Tyche.StarterApp.Identity;

public record LoginRequestDto
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsInvalid() => string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password);
}