namespace Src.Domain.Submission;

public record SikhEventRecord : SubmissionRecord
{
    public int SubmissionId { get; init; }
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public byte[] Image { get; init; } = default!;
    public string Location { get; init; } = default!;
    public string ContactInfo { get; init; } = default!;
    public DateTime EventDateTime { get; init; }
    public float lat { get; init; }
    public float lon { get; init; }

    public override string SubmissionCategory => "SikhEventRecord";
}