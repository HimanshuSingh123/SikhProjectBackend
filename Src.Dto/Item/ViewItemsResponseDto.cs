namespace Src.Dto.Item;

public record ViewItemsResponseDto
{
    public required string Title { get; init; }
    public required byte[] Image { get; init; }
    public required decimal Price { get; init; }
    public required bool IsFavourited { get; init; }
}

