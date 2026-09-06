namespace Src.Dto.MerchItems;

public class SearchMerchItemRequestDto
{
    public string? Title { get; init; }

    public int PageSize { get; init; } = 20;

    public int Page { get; init; } = 1;

    public string? Size { get; init; }
    public double? Rating { get; init; }
    public double? Price { get; init; }

}