using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Entities;
using Src.Domain.Newsfeed;
using Src.Domain.NewsFeed;
using Src.Infrastructure.Persistance;
using System.Diagnostics.CodeAnalysis;

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

    public async Task<bool> UpdateNewsFeedRecord(UpdateNewsFeedRequest request, CancellationToken cancellationToken)
    {
        var newsfeedRecord = await _dbContext.Newsfeed.SingleOrDefaultAsync(nf => nf.SubmissionId == request.SubmissionId, cancellationToken);

        if(newsfeedRecord == null)
        {
            return false;
        }

        var materials = request.getMaterials();

        foreach(var key in materials.Keys)
        {
            switch (key)
            {
                case "Title":
                    if (NullCheck(request.Title)) continue;

                    newsfeedRecord.Title = request.Title;
                    break;
                case "Content":
                    if (NullCheck(request.Content)) continue;

                    newsfeedRecord.Content = request.Content;
                    break;
                case "Description":
                    if (NullCheck(request.Description)) continue;

                    newsfeedRecord.Description = request.Description;
                    break;
                case "Image":
                    if (NullCheck(request.Image)) continue;

                    newsfeedRecord.Image = request.Image;
                    break;
                case "Alert":
                    if (NullCheck(request.Alert)) continue;

                    newsfeedRecord.Alert = (bool)request.Alert;
                    break;
                default:
                    throw new ArgumentException($"Invalid key in UpdateNewsFeedRecord Repository Method: {key}", nameof(key));
            }
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    private static bool NullCheck([NotNullWhen(false)]object? item)
    {
        if(item == null)
        {
            return true;
        }
        return false;
    }
}

