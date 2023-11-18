namespace Web.Models;

public class User
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    //TODO this is temp solution to hold the data when token is generated and then validate it for the user
    //place it somewhere else
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
}