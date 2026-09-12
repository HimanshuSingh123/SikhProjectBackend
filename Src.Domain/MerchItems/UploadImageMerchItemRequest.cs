namespace Src.Domain.MerchItems;

public record UploadImageMerchItemRequest
{
    public required int SubmissionId { get; init; }
    public required byte[] UploadedImage { get; init; }
}

