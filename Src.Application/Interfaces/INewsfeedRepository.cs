using Src.Domain.Newsfeed;
using Src.Domain.NewsFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Interfaces;

public interface INewsfeedRepository
{
    public Task<GetNewsfeedResponse?> GetNewsfeedBySubmissionId(int SubmissionId, CancellationToken cancellationToken);

    public Task<bool> CreateNewsFeedRecord(CreateNewsFeedRecordRequest request, CancellationToken cancellationToken);
}

