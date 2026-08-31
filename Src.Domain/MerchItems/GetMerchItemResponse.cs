namespace Src.Domain.MerchItems;

public record GetMerchItemResponse
{
    public required int SubmissionId { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public byte[]? Image { get; init; }

    public required string Size { get; init; }

    public required int QuantityMax { get; init; }

    public required int QuantityMin { get; init; }

    public required double Price { get; init; }

    public double? Rating { get; init; }
}

