using EFRelations.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFRelations.Infrastructure.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
            : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(s => 
            {
                s.HasKey(k => k.StudentId);
                s.HasData(new Student[]
                {
                    new Student()
                {
                    StudentId = 1,
                    Birthday = new DateTime(1989, 5, 24),
                    PhoneNumber = "76876876868",
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    StudentId = 2,
                    Birthday = new DateTime(1989, 3, 12),
                    PhoneNumber = "76876876868",
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    StudentId = 3,
                    Birthday = new DateTime(1989, 9, 12),
                    PhoneNumber = "76876876868",
                    RegisteredOn = DateTime.Now
                }
                });

                s.OwnsOne(o => o.Name)
                .HasData(new 
                {
                    StudentId = 1,
                    FirstName = "Petar",
                    LastName = "Ivanov" 
                },
                new
                {
                    StudentId = 2,
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                },
                new
                {
                    StudentId = 3,
                    FirstName = "Ivan",
                    LastName = "Petrov"
                });
            });

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost,8976;Database=StudentSystem;User=adotest;Password=1234",
        //                s => s.MigrationsAssembly("EFRelations.Infrastructure"));

        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Student> Students { get; set; }
    }
}
