using Core.Abstractions;
using Core.Models;
using MediatR;

namespace Core.Handlers;

public class GetRequestsCommandHandler : IRequestHandler<GetRequestsCommand, Result<List<Request>, Error>>
{
    private readonly IUnitOfWork _uow;

    public GetRequestsCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result<List<Request>, Error>> Handle(GetRequestsCommand request, CancellationToken cancellationToken)
    {
        return await _uow.RequestRepository.GetAllAsync(cancellationToken);
    }
}

public record GetRequestsCommand() : IRequest<Result<List<Request>, Error>>;