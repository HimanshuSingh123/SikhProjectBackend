using Microsoft.AspNetCore.Mvc;
using Src.Api.ModelBinders;
using Src.Dto.Common.ModelBinders;

namespace Src.Dto.Course;

public record GetDownloadedLessonRequestDto
{
    public required int SubmissionId { get; init; }
    [ModelBinder(BinderType = typeof(LessonTypeCheck))]
    public required string LessonType { get; init; }
}

