namespace AdvancedQuerying
{
    using Microsoft.EntityFrameworkCore;

    public class AdvancedDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Town> Towns { get; set; }


        public AdvancedDbContext()
        {
        }

        public AdvancedDbContext(DbContextOptions<AdvancedDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\AdvancedQueryingDemo_Data.mdf;Database=AdvancedQueryingDemo;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Town>()
                .HasData(new Town()
                {
                    TownId = 1,
                    Name = "Sofia"
                });

            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = 1,
                    Username = "Stamo",
                    Password = "secret",
                    TownId = 1
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
