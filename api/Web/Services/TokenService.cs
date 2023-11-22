using Core.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Web.Models;

namespace Web.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _uow;

    private const string _refreshTokenKey = "refreshToken";

    public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUnitOfWork uow)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _uow = uow;
    }

    public string CreateAccessToken(string username)
    {
        var sectionKey = "AppSettings:Token";
        var expires = DateTime.UtcNow.AddHours(2);

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, username),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection(sectionKey).Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: creds
            ); ;

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public string GetCurrentRefreshToken()
    {
        return _httpContextAccessor?.HttpContext?.Request.Cookies[_refreshTokenKey] ??
            throw new InvalidOperationException();
    }

    public async Task<string> SetRefreshTokenAsync(string username)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var expires = DateTime.UtcNow.AddDays(15);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires,
            SameSite = SameSiteMode.None,
            Secure = true
        };

        _httpContextAccessor?.HttpContext?.Response.Cookies.Append(_refreshTokenKey, token, cookieOptions);

        _uow.UsersRepository.SetRefreshToken(username, token, expires);
        await _uow.CompleteAsync();

        return token;
    }
}
