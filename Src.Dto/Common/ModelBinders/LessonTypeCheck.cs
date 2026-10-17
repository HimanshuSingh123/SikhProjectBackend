using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;

namespace Src.Dto.Common.ModelBinders;

public class LessonTypeCheck : IModelBinder
{
    private List<string> lessonTypes = new()
    {
        "Introduction_Material",
        "WritingLesson_Material",
        "ReadingLesson_Material",
        "SpeakingLesson_Material",
        "Conclusion_Material"
    };

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var LessonType = bindingContext.HttpContext.Request.Query["LessonType"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(LessonType))
        {
            bindingContext.ModelState.AddModelError(
                bindingContext.ModelName,
                "Lesson type is required.");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
        if (!lessonTypes.Contains(LessonType))
        {
            bindingContext.ModelState.AddModelError(
                bindingContext.ModelName,
                "Invalid lesson type.");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
        bindingContext.Result = ModelBindingResult.Success(LessonType);
        return Task.CompletedTask;
    }
}

