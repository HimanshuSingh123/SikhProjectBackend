namespace Src.Domain.Favourite;

public record DeleteFavouritesRequest
{
    public required int Fav_Id { get; init; }
    public required string Username { get; init; }
}

