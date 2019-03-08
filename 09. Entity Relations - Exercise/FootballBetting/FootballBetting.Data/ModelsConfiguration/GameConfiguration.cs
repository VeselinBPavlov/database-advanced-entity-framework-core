namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.GameId);

            builder.HasOne(g => g.HomeTeam)
                   .WithMany(t => t.HomeGames)
                   .HasForeignKey(g => g.HomeTeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.AwayTeam)
                   .WithMany(t => t.AwayGames)
                   .HasForeignKey(g => g.AwayTeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Games");
        }
    }
}