namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);

            builder.Property(p => p.Name)
                   .IsRequired(true)
                   .IsUnicode(true)
                   .HasMaxLength(100);

            builder.Property(p => p.PhoneNumber)
                   .IsRequired(false)
                   .IsUnicode(false)
                   .HasColumnType("CHAR(10)");

            builder.Property(p => p.RegisteredOn)
                   .IsRequired(true);

            builder.Property(p => p.Birthday)
                   .IsRequired(false);

            builder.HasMany(e => e.HomeworkSubmissions)
                   .WithOne(hs => hs.Student)
                   .HasForeignKey(hs => hs.StudentId);

            builder.ToTable("Students");
        }
    }
}