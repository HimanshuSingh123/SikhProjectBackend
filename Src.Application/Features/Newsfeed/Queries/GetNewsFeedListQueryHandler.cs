using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Features.Newsfeed.NewFolder;
using Src.Application.Interfaces;
using Src.Domain.Newsfeed;

namespace Src.Application.Features.Newsfeed.Queries;

public class GetNewsFeedListQueryHandler
    : IRequestHandler<GetNewsFeedListQuery, IReadOnlyList<GetNewsfeedResponse>?>
{
    private readonly INewsfeedRepository _repository;
    private readonly ILogger<GetNewsFeedListQueryHandler> _logger;

    public GetNewsFeedListQueryHandler(
        INewsfeedRepository repository,
        ILogger<GetNewsFeedListQueryHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IReadOnlyList<GetNewsfeedResponse>?> Handle(
        GetNewsFeedListQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "User {Username} requested newsfeed list: PageNumber {PageNumber}, PageSize {PageSize}",
            request.Username,
            request.pageNumber,
            request.pageSize);

        try
        {
            var result = await _repository.GetNewsfeeds(
                request.pageSize,
                request.pageNumber,
                cancellationToken);

            _logger.LogInformation(
                "Newsfeed list retrieval completed for user {Username}: PageNumber {PageNumber}, PageSize {PageSize}, Count {Count}",
                request.Username,
                request.pageNumber,
                request.pageSize,
                result?.Count);

            return result;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Newsfeed list retrieval cancelled for user {Username}: PageNumber {PageNumber}, PageSize {PageSize}",
                request.Username,
                request.pageNumber,
                request.pageSize);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to retrieve newsfeed list for user {Username}: PageNumber {PageNumber}, PageSize {PageSize}",
                request.Username,
                request.pageNumber,
                request.pageSize);
            throw;
        }
    }
}