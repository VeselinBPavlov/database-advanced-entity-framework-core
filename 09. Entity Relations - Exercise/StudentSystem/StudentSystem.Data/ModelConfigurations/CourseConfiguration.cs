namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            builder.Property(p => p.Name)
                   .IsRequired(true)
                   .IsUnicode(true)
                   .HasMaxLength(80);

            builder.Property(p => p.Description)
                   .IsRequired(false)
                   .IsUnicode(true);

            builder.Property(p => p.StartDate)
                   .IsRequired(true);

            builder.Property(p => p.EndDate)
                .IsRequired(true);

            builder.Property(p => p.Price)
                   .IsRequired(true)
                   .HasColumnType("DECIMAL(15, 2)");

            builder.HasMany(c => c.Resources)
                   .WithOne(r => r.Course)
                   .HasForeignKey(r => r.CourseId);

            builder.HasMany(c => c.HomeworkSubmissions)
                   .WithOne(hs => hs.Course)
                   .HasForeignKey(hs => hs.CourseId);

            builder.ToTable("Courses");
        }
    }
}