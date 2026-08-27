using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Favourite.Commands;

public class DeleteFromFavouritesCommandHandler : IRequestHandler<DeleteFromFavouritesCommand, bool>
{
    private readonly ILogger<DeleteFromFavouritesCommandHandler> _logger;
    private readonly IFavouriteRepository _repository;

    public DeleteFromFavouritesCommandHandler(
        ILogger<DeleteFromFavouritesCommandHandler> logger,
        IFavouriteRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(DeleteFromFavouritesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting favourite {FavouriteId} for user {Username}",
            request.Fav_Id,
            request.Username);

        var result = await _repository.DeleteFromFavouritesAsync(request, cancellationToken);

        if (result)
        {
            _logger.LogInformation(
                "Successfully deleted favourite {FavouriteId} for user {Username}",
                request.Fav_Id,
                request.Username);
        }
        else
        {
            _logger.LogWarning(
                "Failed to delete favourite {FavouriteId} for user {Username}",
                request.Fav_Id,
                request.Username);
        }

        return result;
    }
}