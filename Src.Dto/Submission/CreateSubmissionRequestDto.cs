using System.ComponentModel.DataAnnotations;

namespace Src.Dto.Submission;

public record CreateSubmissionRequestDto
{
    [AllowedValues("Merch", "Course", ErrorMessage = "Invalid Category")]
    public required string Category { get; init; }
    [AllowedValues("Pending", "Approved", ErrorMessage ="Invalid Status values")]
    public required string Status { get; init; }
}

