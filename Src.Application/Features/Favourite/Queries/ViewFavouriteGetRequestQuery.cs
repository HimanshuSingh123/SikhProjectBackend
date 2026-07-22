using MediatR;
using Src.Domain.Favourite;
using Src.Domain.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Favourite.Queries;

public record ViewFavouriteGetRequestQuery : ViewFavouritesRequest , IRequest<IEnumerable<ViewFavouritesResponse>>;
