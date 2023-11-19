namespace Core.Models;

public class Request
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public List<UserRequest> UserRequests { get; } = new();
}
