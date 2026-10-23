namespace Src.Domain.Submission;

public record NewsfeedRecord : SubmissionRecord
{
    public int SubmissionId { get; init; }
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public byte[] Image { get; init; } = default!;
    public bool Alert { get; init; }

    public override string SubmissionCategory => "NewsfeedRecord";
}