using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Favourite;

namespace Src.Application.Features.Favourite.Queries;

public class ViewFavouriteGetRequestQueryHandler : IRequestHandler<ViewFavouriteGetRequestQuery, IEnumerable<ViewFavouritesResponse>>
{
    private readonly ILogger<ViewFavouriteGetRequestQueryHandler> _logger;
    private readonly IFavouriteRepository _repository;

    public ViewFavouriteGetRequestQueryHandler(ILogger<ViewFavouriteGetRequestQueryHandler> logger, IFavouriteRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<ViewFavouritesResponse>> Handle(ViewFavouriteGetRequestQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting for user {username}", request.UserId);

        var result = await _repository.GetFavouritesAsync(request);

        _logger.LogInformation("Got {result} favourites from the request", result.Count());

        return result;
    }
}

