using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Course.Queries;

public class AddLessonMaterialQueryHandler : IRequestHandler<AddLessonMaterialQuery, bool>
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<AddLessonMaterialQueryHandler> _logger;

    public AddLessonMaterialQueryHandler(ILogger<AddLessonMaterialQueryHandler> logger, ICourseRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(AddLessonMaterialQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Uploading lesson materials for submission {SubmissionId} for user {Username}", request.Request.SubmissionId, request.Username);

        var result = await _repository.UploadLesson(request.Request, cancellationToken);

        if (result)
        {
            _logger.LogInformation("Successfully processed lesson material upload for submission {SubmissionId} for user {Username}", request.Request.SubmissionId, request.Username);
        }
        else
        {
            _logger.LogWarning("Failed to upload lesson materials for submission {SubmissionId} for user {Username}", request.Request.SubmissionId, request.Username);
        }

        return result;
    }
}