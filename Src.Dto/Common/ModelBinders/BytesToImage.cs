using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Src.Api.ModelBinders;

public class BytesToImage : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var request = bindingContext.HttpContext.Request;
        var fieldName = bindingContext.FieldName;
        var cancellationToken = bindingContext.HttpContext.RequestAborted;

        if (!request.HasFormContentType)
        {
            bindingContext.ModelState.TryAddModelError(fieldName, "Expected Form Data ");

            bindingContext.Result = ModelBindingResult.Failed();
        }

        var form = await request.ReadFormAsync(cancellationToken);
        var file = form.Files.GetFile(fieldName);

        if(file is null)
        {
            return;
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        bindingContext.Result = ModelBindingResult.Success(stream.ToArray());

    }
}

