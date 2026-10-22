using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Submission.Commands;

public class DeleteSubmissionHelperHandler : IRequestHandler<DeleteSubmissionHelper, bool>
{
    private readonly ILogger<DeleteSubmissionHelperHandler> _logger;
    private readonly ISubmissionRepository _repository;

    public DeleteSubmissionHelperHandler(ILogger<DeleteSubmissionHelperHandler> logger, ISubmissionRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(DeleteSubmissionHelper request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submission deletion invoked by user {Username} for submission {SubmissionId}", request.Username, request.SubmissionId);

        try
        {
            bool deleted = await _repository.DeleteSubmission(request.SubmissionId, cancellationToken);

            if (!deleted)
            {
                _logger.LogWarning("Submission deletion requested by user {Username}: submission {SubmissionId} was not found", request.Username, request.SubmissionId);
                return false;
            }

            _logger.LogInformation("Deleted submission {SubmissionId}, requested by user {Username}", request.SubmissionId, request.Username);
            return true;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Submission deletion cancelled for submission {SubmissionId}, requested by user {Username}", request.SubmissionId, request.Username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete submission {SubmissionId}, requested by user {Username}", request.SubmissionId, request.Username);
            throw;
        }
    }
}