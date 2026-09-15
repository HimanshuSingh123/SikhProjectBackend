using Src.Domain.Entities;

namespace Src.Domain.Couse;

public record CreateCourseRequest
{
    public required int SubmissionId { get; init; }

    public required string CourseName { get; init; }

    public required string Description { get; init; }

    public required byte[] Image { get; init; }

    public required string Type { get; init; }

    public required double Price { get; init; }

    public byte[]? IntroductionText { get; init; }
    public byte[]? IntroductionVideo { get; init; }

    public byte[]? WritingLessonText { get; init; }
    public byte[]? WritingLessonVideo { get; init; }

    public byte[]? ReadingLessonText { get; init; }
    public byte[]? ReadingLessonVideo { get; init; }

    public byte[]? SpeakingLessonText { get; init; }
    public byte[]? SpeakingLessonVideo { get; init; }

    public byte[]? ConclusionText { get; init; }
    public byte[]? ConclusionVideo { get; init; }

    public Dictionary<string, (byte[]? Text, byte[]? Video)> GetMaterials()
    {
        return new Dictionary<string, (byte[]? Text, byte[]? Video)>
        {
            { "Introduction_Material", (IntroductionText, IntroductionVideo) },
            { "WritingLesson_Material", (WritingLessonText, WritingLessonVideo) },
            { "ReadingLesson_Material", (ReadingLessonText, ReadingLessonVideo) },
            { "SpeakingLesson_Material", (SpeakingLessonText, SpeakingLessonVideo) },
            { "Conclusion_Material", (ConclusionText, ConclusionVideo) }
        };
    }
}