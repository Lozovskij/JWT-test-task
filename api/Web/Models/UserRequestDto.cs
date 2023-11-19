using Core.Models;

namespace Web.Models;

public class UserRequestDto
{
    public string Username { get; set; } = string.Empty;
    public int RequestId { get; set; }
    public string? UserComment { get; set; }

    public UserRequestDto() { }

    public UserRequestDto(UserRequest userRequest)
    {
        Username = userRequest.User.Username;
        RequestId = userRequest.RequestId;
        UserComment = userRequest.UserComment;
    }
}
