using Microsoft.AspNetCore.Mvc;
using Src.Api.ModelBinders;

namespace Src.Domain.Dto;

public record UpdateCourseRequestDto
{
    public required int SubmissionId { get; init; }

    public string? CourseName { get; init; }

    public string? Description { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? Image { get; init; }

    public string? Type { get; init; }

    public double? Price { get; init; }

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