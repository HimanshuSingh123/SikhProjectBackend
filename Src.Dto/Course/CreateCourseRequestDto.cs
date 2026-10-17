using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Src.Api.ModelBinders;

namespace Src.Dto.Couse;

public record CreateCourseRequestDto
{
    public required int SubmissionId { get; init; }

    public required string CourseName { get; init; }

    public required string Description { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public required byte[] Image { get; init; }

    public required string Type { get; init; }

    public required double Price { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? IntroductionText { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? IntroductionVideo { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? WritingLessonText { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? WritingLessonVideo { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? ReadingLessonText { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? ReadingLessonVideo { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? SpeakingLessonText { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? SpeakingLessonVideo { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? ConclusionText { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
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