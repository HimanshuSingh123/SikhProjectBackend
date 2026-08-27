namespace Src.Domain.Favourite;

public record ViewFavouritesResponse
{
    public required string ItemTitle {get; init;}
    public required string ItemDescription {get; init;}
    public required double Price { get; init;}
    public required string Category { get; init;}
    public required int Submission_Id { get; init; }

    public required int FavId { get; init;}

}

