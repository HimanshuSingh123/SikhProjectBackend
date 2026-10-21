namespace Src.Domain.Submission;

public record CreateSubmissionRequest
{
    public required string Category { get; init; }
    public required string Status { get; init; }
}

