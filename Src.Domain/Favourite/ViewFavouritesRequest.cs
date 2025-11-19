namespace Src.Domain.Favourite;

public record ViewFavouritesRequest
{
    public required string Username { get; init; }
}

