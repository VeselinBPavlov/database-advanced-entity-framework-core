namespace EFCodeFirst.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BlogDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            :base(options)
        {
        }
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {            
                optionsBuilder
                    .UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\BlogDb_Data.mdf;Database=BlogDb;Integrated Security=true;", s => s.MigrationsAssembly("EFCodeFirst.Infrastructure"));
            }
        }



    }
}
