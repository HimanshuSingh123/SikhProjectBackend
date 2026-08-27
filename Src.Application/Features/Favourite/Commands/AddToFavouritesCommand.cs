using MediatR;
using Src.Domain.Favourite;

namespace Src.Application.Features.Favourite.Commands;

public record AddToFavouritesCommand : AddToFavouritesRequest, IRequest<bool>;
