namespace Src.Domain.Favourite;

public record ViewFavouritesRequest
{
    public required int UserId { get; init; }
    public required string Username { get; init; }
}

