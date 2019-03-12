namespace EntityRelations
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentContext()
        {
        }

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\Students_Data.mdf;Database=Students;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);

            // We can use object as property and map chiled class properties as colums.
            //modelBuilder.Entity<Student>()
            //    .OwnsOne(s => s.Name);

            // Seed data from Fluent API.
            modelBuilder.Entity<Student>().HasData(new Student[]
            {
                new Student()
                {
                    StudentId = 1,
                    Birthday = new DateTime(1989, 5, 13),
                    Phone = "02222222222",
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    StudentId = 2,
                    Birthday = new DateTime(1990, 6, 13),
                    Phone = "0888888888",
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    StudentId = 3,
                    Birthday = new DateTime(1991, 7, 13),
                    Phone = "0111111111",
                    RegisteredOn = DateTime.Now
                }
            });
        }
    }
}

