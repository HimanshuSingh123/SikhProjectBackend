using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Dto;

public record UpdateCourseRequestDto
{
    public required int SubmissionId { get; init; }

    public string? CourseName { get; init; }

    public string? Description { get; init; }

    public byte[]? Image { get; init; }

    public string? Type { get; init; }

    public double? Price { get; init; }

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

    public Dictionary<string, object?> GetAttributes()
    {
        return new Dictionary<string, object?>
    {
        { "CourseName", CourseName },
        { "Description", Description },
        { "Image", Image },
        { "Type", Type },
        { "Price", Price }
    };
    }
}

