namespace Src.Domain.Favourite;

public record AddToFavouritesRequest
{
    public required string Username { get; init; }
    public required int submissionId { get; init; }
}

