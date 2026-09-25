namespace Src.Domain.Course;

public record AddLessonMaterialRequest
{
    public required int SubmissionId { get; init; }
    public required string LessonType { get; init; }
    public byte[]? IntroductionText { get; set; }
    public byte[]? IntroductionVideo { get; set; }

    public byte[]? WritingLessonText { get; set; }
    public byte[]? WritingLessonVideo { get; set; }

    public byte[]? ReadingLessonText { get; set; }
    public byte[]? ReadingLessonVideo { get; set; }

    public byte[]? SpeakingLessonText { get; set; }
    public byte[]? SpeakingLessonVideo { get; set; }

    public byte[]? ConclusionText { get; set; }
    public byte[]? ConclusionVideo { get; set; }

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

