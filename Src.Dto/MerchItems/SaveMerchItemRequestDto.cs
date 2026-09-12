namespace Src.Dto.MerchItems;

public record SaveMerchItemRequestDto
{
    public required int SubmissionId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public byte[]? Image { get; init; }
    public string? Size { get; init; }
    public int? QuantityMax { get; init; }
    public int? QuantityMin { get; init; }
    public double? Price { get; init; }
    public double? Rating { get; init; }
}