using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Course;

namespace Src.Application.Features.Course.Queries;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, GetCourseResponse?>
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<GetCourseQueryHandler> _logger;

    public GetCourseQueryHandler(ILogger<GetCourseQueryHandler> logger, ICourseRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetCourseResponse?> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving course for submission {SubmissionId} for user {Username}", request.SubmissionId, request.Username);

        var result = await _repository.GetCourse(request.SubmissionId, cancellationToken);

        if (result is not null)
        {
            _logger.LogInformation("Successfully retrieved course for submission {SubmissionId} for user {Username}", request.SubmissionId, request.Username);
        }
        else
        {
            _logger.LogWarning("Course not found for submission {SubmissionId} for user {Username}", request.SubmissionId, request.Username);
        }

        return result;
    }
}