namespace Src.Dto.Favourite;

public record ViewFavouritesRequestDto
{
    public required int UserId { get; init; }
    public required string Username { get; init; }
}

