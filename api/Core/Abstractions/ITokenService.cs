namespace Core.Abstractions;

public interface ITokenService
{
    string CreateAccessToken(string username);
    Task<string> SetRefreshTokenAsync(string username);
    string GetCurrentRefreshToken();
}
