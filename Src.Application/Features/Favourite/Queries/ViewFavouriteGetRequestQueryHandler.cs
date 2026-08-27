using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Application.Interfaces.Common;
using Src.Domain.Favourite;

namespace Src.Application.Features.Favourite.Queries;

public class ViewFavouriteGetRequestQueryHandler : IRequestHandler<ViewFavouriteGetRequestQuery, IEnumerable<ViewFavouritesResponse>>
{
    private readonly ILogger<ViewFavouriteGetRequestQueryHandler> _logger;
    private readonly IFavouriteRepository _repository;

    public ViewFavouriteGetRequestQueryHandler(ILogger<ViewFavouriteGetRequestQueryHandler> logger, IFavouriteRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<ViewFavouritesResponse>> Handle(ViewFavouriteGetRequestQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting for user {username}", request.Username);

        var result = await _repository.GetFavouritesAsync(request, cancellationToken);

        _logger.LogInformation("Got {result} favourites from the request", result.Count());

        return result;
    }
}

