namespace Src.Domain.Submission;

public record CourseRecord : SubmissionRecord
{
    public int SubmissionId { get; init; }
    public string CourseName { get; init; } = default!;
    public string Description { get; init; } = default!;
    public byte[] Image { get; init; } = default!;
    public string CourseType { get; init; } = default!;
    public double Price { get; init; }

    public override string SubmissionCategory => "CourseRecord";
}