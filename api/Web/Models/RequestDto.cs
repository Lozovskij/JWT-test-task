using Core.Models;

namespace Web.Models;

public class RequestDto
{
    public int RequestId { get; set; }
    public string Description { get; set; } = null!;

    public RequestDto() { }

    public RequestDto(Request request)
    {
        RequestId = request.Id;
        Description = request.Description;
    }
}
