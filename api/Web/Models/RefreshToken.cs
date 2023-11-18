namespace Web.Models;

public class RefreshToken
{
    //TODO active/inactive, db
    public required string Token { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Expires { get; set; }
}
