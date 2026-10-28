using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.Newsfeed.Commands;

public class UpdateNewsfeedCommandHandler : IRequestHandler<UpdateNewsfeedCommand, bool>
{
    private readonly INewsfeedRepository _repository;
    private readonly ILogger<UpdateNewsfeedCommandHandler> _logger;

    public UpdateNewsfeedCommandHandler(
        INewsfeedRepository repository,
        ILogger<UpdateNewsfeedCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(UpdateNewsfeedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "User {Username} requested newsfeed update: SubmissionId {SubmissionId}," +
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
            request.Request.Image?.Length,
            request.Request.Alert);

        try
        {
            bool result = await _repository.UpdateNewsFeedRecord(
                request.Request, cancellationToken);

            _logger.LogInformation(
                "Newsfeed update completed for submission {SubmissionId}, requested by user {Username}, with result {Result}",
                request.Request.SubmissionId,
                request.Username,
                result);

            return result;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Newsfeed update cancelled for submission {SubmissionId}, requested by user {Username}",
                request.Request.SubmissionId,
                request.Username);
            throw;
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(
                ex,
                "Newsfeed update rejected due to an invalid argument for submission {SubmissionId}, requested by user {Username}",
                request.Request.SubmissionId,
                request.Username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to update newsfeed for submission {SubmissionId}, requested by user {Username}",
                request.Request.SubmissionId,
                request.Username);
            throw;
        }
    }
}