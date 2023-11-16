namespace Web.Models;

public class User
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}