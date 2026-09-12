using Microsoft.AspNetCore.Http;

namespace Src.Dto.MerchItems;

public record UploadImageMerchItemRequestDto
{
    public required int SubmissionId { get; init; }
    public required IFormFile UploadedImage { get; init; }
}

