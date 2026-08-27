using MediatR;
using Src.Domain.Favourite;

namespace Src.Application.Features.Favourite.Commands;

public record DeleteFromFavouritesCommand : DeleteFavouritesRequest, IRequest<bool>;

