using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.NewsFeed;

public record UpdateNewsFeedRequest
{
    public required int SubmissionId { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public string? Description { get; init; }
    public byte[]? Image { get; init; }
    public bool? Alert { get; init; }
}

