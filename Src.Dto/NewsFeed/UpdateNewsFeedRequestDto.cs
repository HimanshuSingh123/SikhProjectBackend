namespace Src.Dto.NewsFeed;

public record UpdateNewsFeedRequestDto
{
    public required int SubmissionId { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public string? Description { get; init; }
    public byte[]? Image { get; init; }
    public bool? Alert { get; init; }

    public Dictionary<string, object?> getMaterials()
    {
        return new Dictionary<string, object?>
        {
            { "Title", Title },
            { "Content", Content },
            { "Description", Description },
            { "Image", Image },
            { "Alert", Alert }
        };
    }
}

