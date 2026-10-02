using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Course.Commands;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, bool>
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<UpdateCourseCommandHandler> _logger;

    public UpdateCourseCommandHandler(ILogger<UpdateCourseCommandHandler> logger, ICourseRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating course {SubmissionId} for user {Username}", request.request.SubmissionId, request.Username);

        var result = await _repository.UpdateCourse(request.request, cancellationToken);

        if (result)
        {
            _logger.LogInformation("Successfully updated course {SubmissionId} for user {Username}", request.request.SubmissionId, request.Username);
        }
        else
        {
            _logger.LogWarning("Failed to update course {SubmissionId} for user {Username}", request.request.SubmissionId, request.Username);
        }

        return result;
    }
}