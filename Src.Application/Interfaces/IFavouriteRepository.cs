using Src.Domain.Favourite;

namespace Src.Application.Interfaces;

public interface IFavouriteRepository
{
    Task<IEnumerable<ViewFavouritesResponse>> GetFavouritesAsync(ViewFavouritesRequest request);
}

