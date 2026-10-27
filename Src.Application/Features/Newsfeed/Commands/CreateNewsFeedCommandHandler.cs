using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Newsfeed.Commands;

public class CreateNewsFeedCommandHandler : IRequestHandler<CreateNewsFeedCommand, bool>
{
    private readonly INewsfeedRepository _repository;
    private readonly ILogger<CreateNewsFeedCommandHandler> _logger;

    public CreateNewsFeedCommandHandler(INewsfeedRepository repository, ILogger<CreateNewsFeedCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(CreateNewsFeedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {Username} requested newsfeed creation: SubmissionId {SubmissionId}," +
            " Title {Title}," +
            " Description {Description}," +
            " Content {Content}," +
            " ImageSize {ImageSize} bytes," +
            " Alert {Alert}",
            request.Username,
            request.Request.SubmissionId,
            request.Request.Title,
            request.Request.Description,
            request.Request.Content,
            request.Request.Image?.Length ?? 0,
            request.Request.Alert);

        try
        {
            bool result = await _repository.CreateNewsFeedRecord(request.Request, cancellationToken);

            _logger.LogInformation("Newsfeed creation completed for submission {SubmissionId}, requested by user {Username}, with result {Result}", request.Request.SubmissionId, request.Username, result);

            return result;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Newsfeed creation cancelled for submission {SubmissionId}, requested by user {Username}", request.Request.SubmissionId, request.Username);
            throw;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Newsfeed creation rejected for submission {SubmissionId} with title {Title}, requested by user {Username}", request.Request.SubmissionId, request.Request.Title, request.Username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create newsfeed for submission {SubmissionId}, requested by user {Username}", request.Request.SubmissionId, request.Username);
            throw;
        }
    }
}