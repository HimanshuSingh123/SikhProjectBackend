using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Features.Newsfeed.NewFolder;
using Src.Application.Interfaces;
using Src.Domain.Newsfeed;

namespace Src.Application.Features.Newsfeed.Queries;

public class GetNewsfeedQueryHandler : IRequestHandler<GetNewsfeedQuery, GetNewsfeedResponse?>
{
    private readonly INewsfeedRepository _repository;
    private readonly ILogger<GetNewsfeedQueryHandler> _logger;

    public GetNewsfeedQueryHandler(INewsfeedRepository repository, ILogger<GetNewsfeedQueryHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<GetNewsfeedResponse?> Handle(GetNewsfeedQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User {Username} requested newsfeed for submission {SubmissionId}", request.Username, request.SubmissionId);

        try
        {
            var response = await _repository.GetNewsfeedBySubmissionId(request.SubmissionId, cancellationToken);

            if (response is null)
            {
                _logger.LogWarning("No newsfeed found for submission {SubmissionId}, requested by user {Username}", request.SubmissionId, request.Username);
                return null;
            }

            _logger.LogInformation("Successfully retrieved newsfeed for user {Username}:" +
                " SubmissionId {SubmissionId}," +
                " Title {Title}," +
                " Description {Description}," +
                " Content {Content}," +
                " ImageSize {ImageSize} bytes," +
                " Alert {Alert}",
                request.Username,
                response.SubmissionId,
                response.Title, response.Description,
                response.Content,
                response.Image?.Length ?? 0,
                response.Alert);

            return response;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Newsfeed retrieval cancelled for submission {SubmissionId}, requested by user {Username}", request.SubmissionId, request.Username);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve newsfeed for submission {SubmissionId}, requested by user {Username}", request.SubmissionId, request.Username);
            throw;
        }
    }
}