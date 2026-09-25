using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Course;
using Src.Dto.Course;

namespace Src.Application.Features.Course.Queries;

public class GetDownloadedLessonsQueryHandler : IRequestHandler<GetDownloadedLessonsQuery, GetDownloadedLessonsResponse?>
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<GetDownloadedLessonsQueryHandler> _logger;

    public GetDownloadedLessonsQueryHandler(ILogger<GetDownloadedLessonsQueryHandler> logger, ICourseRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetDownloadedLessonsResponse?> Handle(GetDownloadedLessonsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving lesson {LessonType} for submission {SubmissionId} for user {Username}", request.request.LessonType, request.request.SubmissionId, request.Username);

        var result = await _repository.GetDownloadableLessons(request.request, cancellationToken);

        if (result is not null)
        {
            _logger.LogInformation("Successfully retrieved lesson {LessonType} for submission {SubmissionId} for user {Username}", request.request.LessonType, request.request.SubmissionId, request.Username);
        }
        else
        {
            _logger.LogWarning("Lesson {LessonType} not found for submission {SubmissionId} for user {Username}", request.request.LessonType, request.request.SubmissionId, request.Username);
        }

        return result;
    }
}