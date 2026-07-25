namespace Src.Domain.Entities;

public class RegisteredCourses
{
    public int SubmissionId { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public string Username { get; set; } = default!;

    public Course Course { get; set; } = default!;
    public User User { get; set; } = default!;

}

