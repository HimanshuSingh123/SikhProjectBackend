using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Submission;

namespace Src.Application.Features.Submission.Commands;

public class CreateSubmissionHelperCommandHandler : IRequestHandler<CreateSubmissionHelperCommand, int>
{
    private readonly List<string> allowedCategories = new List<string>() 
    { 
        "Merch",
        "Course"
    };
    private readonly List<string> allowedStatus = new List<string>() 
    { 
        "Pending",
        "Approved"
    };

    private readonly ILogger<CreateSubmissionHelperCommandHandler> _logger;
    private readonly ISubmissionRepository _repository;

    public CreateSubmissionHelperCommandHandler(ILogger<CreateSubmissionHelperCommandHandler> logger, ISubmissionRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> Handle(CreateSubmissionHelperCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating submission for user {Username}, category {Category}, status {Status}", request.Username, request.Request.Category, request.Request.Status);

        if (request.Request.Status == null || request.Request.Category == null || request.Username == null)
        {
            _logger.LogWarning("Submission creation rejected: a required argument is null");
            throw new ArgumentException("No null arguments");
        }

        if (!allowedCategories.Contains(request.Request.Category))
        {
            _logger.LogWarning("Submission creation rejected for user {Username}: unsupported category {Category}", request.Username, request.Request.Category);
            throw new ArgumentException("This type of category is not allowed");
        }

        if (!allowedStatus.Contains(request.Request.Status))
        {
            _logger.LogWarning("Submission creation rejected for user {Username}: unsupported status {Status}", request.Username, request.Request.Status);
            throw new ArgumentException("This type of status is not allowed");
        }

        try
        {
            int submissionId = await _repository.CreateSubmissionHelper(request.Username, request.Request, cancellationToken);

            _logger.LogInformation("Created submission {SubmissionId} for user {Username}", submissionId, request.Username);

            return submissionId;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Submission creation cancelled for user {Username}", request.Username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create submission for user {Username}, category {Category}", request.Username, request.Request.Category);
            throw;
        }
    }
}