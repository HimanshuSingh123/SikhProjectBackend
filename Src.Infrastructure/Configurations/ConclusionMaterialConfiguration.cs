using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities;
using Src.Infrastructure.Configurations.AbstractConfigurations;

namespace Src.Infrastructure.Configurations;

// inheriting from BaseCourseMaterialConfiguration<ConclusionMaterial>, this basically sends ConclusionMaterial as the generic type and gives us the ability
// to configure the base as you see below to apply BaseCourseMaterialConfiguration to this ConclusionMaterialConfiguration class
public class ConclusionMaterialConfiguration : BaseCourseMaterialConfiguration<ConclusionMaterial>
{
    // i want to override to provide extra mapping
    public override void Configure(EntityTypeBuilder<ConclusionMaterial> builder)
    {
        //actually configures the extra mapping from BaseCourseMaterialConfiguration
        base.Configure(builder);

        builder.ToTable("Conclusion_Material");

        builder.HasOne(cm => cm.Course)
            .WithOne(c => c.ConclusionMaterial)
            .HasForeignKey<ConclusionMaterial>(cm => cm.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

