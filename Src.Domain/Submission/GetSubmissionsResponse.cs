namespace Src.Domain.Submission;

public record GetSubmissionsResponse
{
    public required IEnumerable<MerchRecord> MerchRecords { get; init; }
    public required IEnumerable<CourseRecord> CourseRecords { get; init; }
    public required IEnumerable<NewsfeedRecord> NewsfeedRecords { get; init; }
    public required IEnumerable<SikhEventRecord> SikhEventRecords { get; init; }
    public required IEnumerable<SubmissionRecord> SubmissionRecords { get; init; }
}   