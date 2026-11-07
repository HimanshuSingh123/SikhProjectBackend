using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Features.Newsfeed.NewFolder;
using Src.Application.Interfaces;
using Src.Domain.NewsFeed;

namespace Src.Application.Features.Newsfeed.Commands;

public class SikhNewsFeedCommandHandler : IRequestHandler<SikhNewsFeedCommand, bool>
{
    private readonly INewsfeedRepository _repository;
    private readonly ILogger<SikhNewsFeedCommandHandler> _logger;
    private readonly SikhnetNewsFeedHelper _newsfeedHelper;

    public SikhNewsFeedCommandHandler(INewsfeedRepository repository, ILogger<SikhNewsFeedCommandHandler> logger, SikhnetNewsFeedHelper newsfeedHelper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _newsfeedHelper = newsfeedHelper ?? throw new ArgumentNullException(nameof(newsfeedHelper));
    }

    public async Task<bool> Handle(SikhNewsFeedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "User {Username} requested a SikhNet newsfeed import",
            request.Username);

        try
        {
            SikhnetNewsRecord? record = await _newsfeedHelper.getNewestArticle(cancellationToken);

            if (record is null)
            {
                throw new InvalidDataException("The SikhNet feed contained no valid articles.");
            }

            bool newsfeedExists = await _repository.SikhNewsArticleExist(record.SikhNetNewsRecordId, cancellationToken);

            if (newsfeedExists)
            {
                throw new InvalidDataException("The SikhNet article already exists.");
            }

            await _repository.CreateNewsFeedRecord(new CreateNewsFeedRecordRequest
            {
                SubmissionId = 0, // gotta do this - assign the associated submissions ID.
                Alert = true,
                Content = record.Url,
                Image = record.Image,
                Title = record.Title,
                Description = record.Description
            }, cancellationToken);

            _logger.LogInformation(
                "Created newsfeed record for SikhNet article {ArticleId}, requested by user {Username}",
                record.SikhNetNewsRecordId,
                request.Username);

            return true;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "SikhNet newsfeed import cancelled for user {Username}",
                request.Username);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to import SikhNet newsfeed for user {Username}",
                request.Username);

            throw;
        }
    }
}