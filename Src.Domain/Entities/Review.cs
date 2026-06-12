namespace Src.Domain.Entities;

public class Review
{
    public int ReviewId { get; set; } = default!;
    public int SubmissionId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public DateTime ModifiedAt { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string Role { get; set; } = default!;

    public Submission Submission { get; set; } = default!;
    public User User { get; set; } = default!;
}

