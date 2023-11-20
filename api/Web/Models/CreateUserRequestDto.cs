using Core.Models;

namespace Web.Models;

public class CreateUserRequestDto
{
    public string Username { get; set; } = null!;
    public int RequestId { get; set; }
    public string? UserComment { get; set; }
}
