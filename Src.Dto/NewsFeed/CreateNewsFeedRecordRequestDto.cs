using Microsoft.AspNetCore.Mvc;
using Src.Api.ModelBinders;
using Src.Dto.Newsfeed;
namespace Src.Dto.NewsFeed;

public record CreateNewsFeedRecordRequestDto
{
    public required int SubmissionId { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required string Description { get; init; }
    [ModelBinder(BinderType = typeof(BytesToImage))]
    public required byte[] Image { get; init; }
    public required bool Alert { get; init; }
};

