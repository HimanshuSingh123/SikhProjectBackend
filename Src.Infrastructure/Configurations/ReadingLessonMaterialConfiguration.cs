using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using Src.Domain.Entities.AbstractEntities;
using Src.Infrastructure.Configurations.AbstractConfigurations;

namespace Src.Infrastructure.Configurations;

public class ReadingLessonMaterialConfiguration : BaseCourseMaterialConfiguration<ReadingLessonMaterial>
{
    public override void Configure(EntityTypeBuilder<ReadingLessonMaterial> builder)
    {
        base.Configure(builder);

        builder.ToTable("Reading_Lesson_Material");

        builder.HasOne(rlm => rlm.Course)
            .WithOne(c => c.ReadingLessonMaterial)
            .HasForeignKey<ReadingLessonMaterial>(rlm => rlm.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}

