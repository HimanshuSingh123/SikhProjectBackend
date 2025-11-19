using Src.Application.Interfaces;
using Src.Domain.Favourite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Infrastructure.Repository;

public class FavouriteRepository : IFavouriteRepository
{
    public Task<IEnumerable<ViewFavouritesResponse>> GetFavouritesAsync(ViewFavouritesRequest request)
    {
        throw new NotImplementedException();
    }
}

