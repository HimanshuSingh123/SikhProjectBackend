namespace Src.Domain.MerchItems;

public record MerchDataItem
{
    public int SubmissionId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public byte[]? Image { get; set; } = default!;
    public string Size { get; set; } = default!;
    public int QuantityMax { get; set; }
    public int QuantityMin { get; set; }
    public double Price { get; set; }
    public double? Rating { get; set; }
}

