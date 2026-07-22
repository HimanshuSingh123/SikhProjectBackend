using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain.Entities.AbstractEntities;

namespace Src.Infrastructure.Configurations.AbstractConfigurations;
// this is the base configuation for any entity that inherits BaseCourseMaterial
// concrete classes can inherit this to replicate the same exact mappings
public abstract class BaseCourseMaterialConfiguration<MaterialType> : IEntityTypeConfiguration<MaterialType> where MaterialType : BaseCourseMaterial
{
    //virtual here because i want configure to be able to be redefined if it needs more complete mapping or more relationships established
    // only check conclusionmaterialconfig for the "other side" of this
    public virtual void Configure(EntityTypeBuilder<MaterialType> builder)
    {
        builder.HasKey(BaseCourseMaterial => BaseCourseMaterial.SubmissionId);

        builder.Property(bcm => bcm.SubmissionId)
            .HasColumnName("submission_id");

        builder.Property(bcm => bcm.UploadedMaterial)
            .HasColumnName("uploaded_material");

        builder.Property(bcm => bcm.UploadedMaterial)
            .HasColumnName("video_material");

        builder.Property(bcm => bcm.CreatedAt)  
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(bcm => bcm.ModifiedAt)
            .HasColumnName("modified_at");
    }
}

