using Src.Domain.Entities;
using Src.Domain.Favourite;

namespace Src.Application.Interfaces;

public interface IFavouriteRepository
{
    Task<IEnumerable<ViewFavouritesResponse>> GetFavouritesAsync(ViewFavouritesRequest request, CancellationToken cancellationToken);

    Task<bool> AddToFavouritesAsync(AddToFavouritesRequest request, CancellationToken cancellationToken);

    Task<Favourites?> SubmissionExtractor(AddToFavouritesRequest request, CancellationToken cancellationToken);

    Task<bool> DeleteFromFavouritesAsync(DeleteFavouritesRequest request, CancellationToken cancellationToken);
}

