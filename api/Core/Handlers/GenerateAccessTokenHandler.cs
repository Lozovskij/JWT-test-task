using Core.Abstractions;
using MediatR;

namespace Core.Handlers;

public class GenerateAccessTokenHandler : IRequestHandler<GenerateAccessTokenCommand, Result<string, Error>>
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateAccessTokenHandler(ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string, Error>> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = _tokenService.GetCurrentRefreshToken();
        var user = await _unitOfWork.UsersRepository.TryGetUserByNameAsync(request.Username, cancellationToken);
        if (user == null)
        {
            return new Error("Can't find the user. Something went wrong", HttpStatusCode.NotFound);
        }

        if (!user.RefreshToken.Equals(refreshToken))
        {
            return new Error("Invalid refresh token", HttpStatusCode.Unauthorized);
        }
        else if (user.TokenExpires < DateTime.UtcNow)
        {
            return new Error("Refresh token expired", HttpStatusCode.Unauthorized);
        }

        string token = _tokenService.CreateAccessToken(user.Username);

        return token;
    }
}

public record GenerateAccessTokenCommand(string Username) : IRequest<Result<string, Error>>;
