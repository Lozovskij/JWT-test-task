namespace Core;

public class Error
{
    public string Message { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public Error(string message, HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
}

public enum HttpStatusCode
{
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    InternalServerError = 500
    // Add more status codes as needed
}