using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Entities;
using Src.Domain.Newsfeed;
using Src.Domain.NewsFeed;
using Src.Infrastructure.Persistance;

namespace Src.Infrastructure.Repository;

public class NewsfeedRepository : INewsfeedRepository
{
    private readonly ApplicationDbContext _dbContext;
    public NewsfeedRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<GetNewsfeedResponse?> GetNewsfeedBySubmissionId(int SubmissionId, CancellationToken cancellationToken)
    {
        var newsFeedResult = await _dbContext.Newsfeed.FirstOrDefaultAsync(nf => nf.SubmissionId == SubmissionId, cancellationToken);

        if (newsFeedResult is null)
        {
            return null;
        }

        return new GetNewsfeedResponse
        {
            SubmissionId = newsFeedResult.SubmissionId,
            Title = newsFeedResult.Title,
            Content = newsFeedResult.Content,
            Description = newsFeedResult.Description,
            Image = newsFeedResult.Image,
            Alert = newsFeedResult.Alert
        };
    }

    public async Task<bool> CreateNewsFeedRecord(CreateNewsFeedRecordRequest request, CancellationToken cancellationToken)
    {
        var uniqueTitle = await _dbContext.Newsfeed.AnyAsync(nf => nf.Title == request.Title, cancellationToken);
        if (uniqueTitle)
        {
            throw new InvalidOperationException("A newsfeed post with this title already exists.");
        }
        if (request.Alert == false)
        {
            throw new InvalidOperationException("A new newsfeed post must be tagged with alert = true.");
        }
        var newsfeed = new Newsfeed
        {
            SubmissionId = request.SubmissionId,
            Title = request.Title,
            Content = request.Content,
            Description = request.Description ?? "",
            Image = request.Image ?? [],
            Alert = request.Alert // this will always have to be true for a new newsfeed obv
        };

        _dbContext.Newsfeed.Add(newsfeed);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public Task<bool> UpdateNewsFeedRecord(UpdateNewsFeedRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

