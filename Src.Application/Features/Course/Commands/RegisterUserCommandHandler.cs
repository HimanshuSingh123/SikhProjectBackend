using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Course.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(
        ILogger<RegisterUserCommandHandler> logger,
        ICourseRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Registering user {Username} for course {SubmissionId}",
            request.Username, request.SubmissionId);

        var result = await _repository.RegisterUser(
            request.Username, request.SubmissionId, cancellationToken);

        if (result)
        {
            _logger.LogInformation(
                "Successfully registered user {Username} for course {SubmissionId}",
                request.Username, request.SubmissionId);
        }
        else
        {
            _logger.LogWarning(
                "Failed to register user {Username} for course {SubmissionId}",
                request.Username, request.SubmissionId);
        }

        return result;
    }
}