namespace Src.Domain.Entities;

public class RegisteredCourses
{
    public int SubmissionId { get; init; }
    public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;
    public string Username { get; init; } = default!;

    public Course Course { get; init; } = default!;
    public User User { get; set; } = default!;

}

