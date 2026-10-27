namespace Src.Domain.Newsfeed;

public record GetNewsfeedResponse
{
    public required int SubmissionId { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required string Description { get; init; }
    public required byte[] Image { get; init; }
    public required bool Alert { get; init; }
}