namespace Src.Dto.Item;

public record ViewItemsRequestDto
{
    public string? searchData { get; init; }
    public int? pageSize { get; init; }
    public int? pages { get; init; }
    public decimal? priceMax { get; init; }
    public decimal? priceMin { get; init; }
    public int? sizeMin { get; init; }
    public int? sizeMax { get; init; }
    public string? category { get; init; }
    public bool? ascending { get; init; }
    public bool? descending { get; init; }

}

