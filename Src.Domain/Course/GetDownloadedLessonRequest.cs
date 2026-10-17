namespace Src.Domain.Course;

public record GetDownloadedLessonRequest
{
    public required int SubmissionId { get; init; }
    public required string LessonType { get; init; }
}

