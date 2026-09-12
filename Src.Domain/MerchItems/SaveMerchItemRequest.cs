namespace Src.Domain.MerchItems;

public record SaveMerchItemRequest
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

    public Dictionary<string, object?> AttributesToDictionary()
    {
        return new Dictionary<string, object?>
        {
            { "SubmissionId", SubmissionId },
            { "Title", Title },
            { "Description", Description },
            { "Image", Image },
            { "Size", Size },
            { "QuantityMax", QuantityMax },
            { "QuantityMin", QuantityMin },
            { "Price", Price },
            { "Rating", Rating }
        };
    }
}