using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using Src.Infrastructure.Configurations.AbstractConfigurations;

namespace Src.Infrastructure.Configurations;

public class IntroductionMaterialConfiguration : BaseCourseMaterialConfiguration<IntroductionMaterial>
{
    public override void Configure(EntityTypeBuilder<IntroductionMaterial> builder)
    {
        base.Configure(builder);

        builder.ToTable("Introduction_Material");

        builder.HasOne(im => im.Course)
            .WithOne(c => c.IntroductionMaterial)
            .HasForeignKey<IntroductionMaterial>(im => im.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

