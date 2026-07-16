using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using Src.Infrastructure.Configurations.AbstractConfigurations;

namespace Src.Infrastructure.Configurations;

public class WritingLessonMaterialConfiguration : BaseCourseMaterialConfiguration<WritingLessonMaterial>
{
    public override void Configure(EntityTypeBuilder<WritingLessonMaterial> builder)
    {
        base.Configure(builder);

        builder.ToTable("Writing_Lesson_Material");

        builder.HasOne(wlm => wlm.Course)
            .WithOne(c => c.WritingLessonMaterial)
            .HasForeignKey<WritingLessonMaterial>(wlm => wlm.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

