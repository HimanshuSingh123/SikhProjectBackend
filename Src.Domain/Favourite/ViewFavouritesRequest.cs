namespace Src.Domain.Favourite;

public record ViewFavouritesRequest
{
    public required int UserId { get; init; }
}

