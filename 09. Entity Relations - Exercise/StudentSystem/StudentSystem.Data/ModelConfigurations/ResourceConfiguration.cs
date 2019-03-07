namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(r => r.ResourceId);

            builder.Property(p => p.Name)
                   .IsRequired(true)
                   .IsUnicode(true)
                   .HasMaxLength(50);

            builder.Property(p => p.Url)
                   .IsRequired(true)
                   .IsUnicode(false);

            builder.Property(p => p.ResourceType)
                   .IsRequired(true);

            builder.HasOne(r => r.Course)
                   .WithMany(c => c.Resources)
                   .HasForeignKey(r => r.CourseId);

            builder.ToTable("Resources");
        }
    }
}