namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.TeamId);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(40);

            builder.Property(t => t.Initials)
                   .IsRequired()
                   .HasColumnType("NCHAR(3)");

            builder.Property(t => t.LogoUrl)
                   .IsUnicode(false);

            builder.HasOne(t => t.PrimaryKitColor)
                   .WithMany(c => c.PrimaryKitTeams)
                   .HasForeignKey(t => t.PrimaryKitColorId)
                   .OnDelete(DeleteBehavior.Restrict);            
        
            builder.HasOne(t => t.SecondaryKitColor)
                   .WithMany(c => c.SecondaryKitTeams)
                   .HasForeignKey(t => t.SecondaryKitColorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Town)
                   .WithMany(t => t.Teams)
                   .HasForeignKey(t => t.TownId);

            builder.ToTable("Teams");
        }
    }
}