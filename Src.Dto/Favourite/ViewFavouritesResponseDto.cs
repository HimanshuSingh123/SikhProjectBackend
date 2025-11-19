namespace Src.Dto.Favourite;

public record ViewFavouritesResponseDto
{
    public required string ItemTitle {get; init;}
    public required string ItemDescription {get; init;}
    public required double Price { get; init;}
    public required string Category { get; init;}

    public required int FavId { get; init;}

}

