using MediatR;
using Src.Domain.Favourite;
using Src.Domain.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Query.Favourite;

public record ViewFavouriteGetRequestQuery : ViewFavouritesRequest , IRequest<IEnumerable<ViewFavouritesResponse>>;
