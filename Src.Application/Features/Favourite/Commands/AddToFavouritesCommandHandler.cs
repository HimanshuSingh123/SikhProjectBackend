using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Favourite.Commands;

public class AddToFavouritesCommandHandler : IRequestHandler<AddToFavouritesCommand, bool>
{
    private readonly IFavouriteRepository _repository;
    private readonly ILogger<AddToFavouritesCommandHandler> _logger;

    public AddToFavouritesCommandHandler(ILogger<AddToFavouritesCommandHandler> logger, IFavouriteRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(AddToFavouritesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding submission {SubmissionId} to favourites for user {Username}", request.submissionId, request.Username);

        var result = await _repository.AddToFavouritesAsync(request, cancellationToken);

        if (result)
        {
            _logger.LogInformation("Successfully added submission {SubmissionId} to favourites for user {Username}", request.submissionId, request.Username);
        }
        else
        {
            _logger.LogWarning("Failed to add submission {SubmissionId} to favourites for user {Username}", request.submissionId, request.Username);
        }

        return result;
    }
}