namespace Core.Constants;

internal class Errors
{
    public static readonly Error NoCurrentUser = new("Can't determine current user", HttpStatusCode.Unauthorized);
}
