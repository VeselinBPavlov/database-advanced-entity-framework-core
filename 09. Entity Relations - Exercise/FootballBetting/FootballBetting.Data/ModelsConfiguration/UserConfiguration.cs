namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Name)
                   .HasMaxLength(100);

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(60);

            builder.ToTable("Users");
        }
    }
}