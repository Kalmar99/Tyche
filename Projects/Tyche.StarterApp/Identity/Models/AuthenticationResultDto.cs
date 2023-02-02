namespace Tyche.StarterApp.Identity.Models;

public class AuthenticationResultDto
{
    public AuthenticationResultDto(string token, string error)
    {
        Token = token;
        Error = error;
    }
    
    public string Token { get; }

    public string Error { get; }
}