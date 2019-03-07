namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(h => h.HomeworkId);

            builder.Property(p => p.Content)
                   .IsRequired(true)
                   .IsUnicode(false)
                   .HasColumnType("NVARCHAR(MAX)");

            builder.Property(p => p.ContentType)
                   .IsRequired(true);

            builder.Property(p => p.SubmissionTime)
                   .IsRequired(true);

            builder.HasOne(h => h.Student)
                   .WithMany(s => s.HomeworkSubmissions)
                   .HasForeignKey(h => h.StudentId);

            builder.HasOne(h => h.Course)
                   .WithMany(c => c.HomeworkSubmissions)
                   .HasForeignKey(c => c.CourseId);

            builder.ToTable("HomeworkSubmissions");
        }
    }
}