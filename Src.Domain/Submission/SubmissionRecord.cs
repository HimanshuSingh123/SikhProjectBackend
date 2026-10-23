namespace Src.Domain.Submission;

public abstract record SubmissionRecord
{
    public abstract String SubmissionCategory { get; }
}

