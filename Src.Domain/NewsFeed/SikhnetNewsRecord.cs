namespace Src.Domain.NewsFeed;

public record SikhnetNewsRecord
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Url { get; init; }
    public required int SikhNetNewsRecordId { get; init; }
    public required DateTime PublicationDate { get; init; }
    public required byte[] Image { get; init; }
}

