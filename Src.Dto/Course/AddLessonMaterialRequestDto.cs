using Microsoft.AspNetCore.Mvc;
using Src.Api.ModelBinders;

namespace Src.Dto.Course;

public record   AddLessonMaterialRequestDto
{
    public required int SubmissionId { get; init; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? IntroductionText { get; set; }
    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? IntroductionVideo { get; set; }
    
    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? WritingLessonText { get; set; }
    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? WritingLessonVideo { get; set; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? ReadingLessonText { get; set; }
    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? ReadingLessonVideo { get; set; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? SpeakingLessonText { get; set; }
    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? SpeakingLessonVideo { get; set; }

    [ModelBinder(BinderType = typeof(BytesToImage))]
    public byte[]? ConclusionText { get; set; }
    [ModelBinder(BinderType = typeof(BytesToImage))]
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

