using Core.Models;

namespace Web.Models;

public class UserRequestDto
{
    public int UserRequestId { get; set; }
    public string Username { get; set; } = null!;
    public int RequestId { get; set; }
    public string? UserComment { get; set; }
    public DateTime CreatedDate { get; set; }
    public string RequestDescription { get; set; } = null!;
    public RequestStatus RequestStatus { get; set; }

    public UserRequestDto() { }

    public UserRequestDto(UserRequest userRequest)
    {
        UserRequestId = userRequest.Id;
        Username = userRequest.User.Username;
        RequestId = userRequest.RequestId;
        UserComment = userRequest.UserComment;
        CreatedDate = userRequest.CreatedDate;
        RequestDescription = userRequest.Request.Description;
        RequestStatus = userRequest.Status;
    }
}
