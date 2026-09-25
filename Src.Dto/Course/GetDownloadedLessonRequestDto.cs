namespace Src.Dto.Course;

public record GetDownloadedLessonRequestDto
{
    public required int SubmissionId { get; init; }
    public required string LessonType { get; init; }
}


