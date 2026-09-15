using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Course;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<CreateCourseCommandHandler> _logger;

    public CreateCourseCommandHandler(ILogger<CreateCourseCommandHandler> logger, ICourseRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating course for submission {SubmissionId} for user {Username}", request.Request.SubmissionId, request.Username);

        var result = await _repository.CreateCourse(request.Request, cancellationToken);

        if (result)
        {
            _logger.LogInformation("Successfully created course for submission {SubmissionId} for user {Username}", request.Request.SubmissionId, request.Username);
        }
        else
        {
            _logger.LogWarning("Failed to create course for submission {SubmissionId} for user {Username}", request.Request.SubmissionId, request.Username);
        }

        return result;
    }
}