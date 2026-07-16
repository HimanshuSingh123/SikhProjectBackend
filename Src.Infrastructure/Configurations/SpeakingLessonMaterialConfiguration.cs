using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using Src.Infrastructure.Configurations.AbstractConfigurations;
namespace Src.Infrastructure.Configurations;

public class SpeakingLessonMaterialConfiguration : BaseCourseMaterialConfiguration<SpeakingLessonMaterial>
{
    public override void Configure(EntityTypeBuilder<SpeakingLessonMaterial> builder)
    {
        base.Configure(builder);

        builder.ToTable("Speaking_Lesson_Material");

        builder.HasOne(slm => slm.Course)
            .WithOne(c => c.SpeakingLessonMaterial)
            .HasForeignKey<SpeakingLessonMaterial>(slm => slm.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
            
    }
}

