namespace Src.Domain.Submission;

public record MerchRecord : SubmissionRecord
{
    public int SubmissionId { get; init; }
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public byte[]? Image { get; init; }
    public string Size { get; init; } = default!;
    public int QuantityMax { get; init; }
    public int QuantityMin { get; init; }
    public double Price { get; init; }
    public double? Rating { get; init; }

    public override string SubmissionCategory => "MerchRecord";
}