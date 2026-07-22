namespace Src.Domain.Entities.AbstractEntities;

public abstract class BaseCourseMaterial
{
    public int SubmissionId { get; set; }
    public byte[] UploadedMaterial { get; set; } = [];
    public byte[] VideoMaterial { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

    public Course Course { get; set; } = default!;
}

