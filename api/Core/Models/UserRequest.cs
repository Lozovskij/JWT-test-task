namespace Core.Models;

public class UserRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RequestId { get; set; }
    public DateTime CreatedDate { get; set; }
    public RequestStatus Status { get; set; }
    public string? UserComment { get; set; }
    public User User { get; set; } = null!;
    public Request Request { get; set; } = null!;
}

public enum RequestStatus
{
    Active,
    Canceled,
    Resolved,
}