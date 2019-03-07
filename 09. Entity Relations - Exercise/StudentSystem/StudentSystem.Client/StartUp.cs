namespace P01_StudentSystem.Client
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Initializer;
    using System;
    using System.Linq;

    public class StartUp
    {
        static void Main()
        {
            // Seed hardcode data.
            // Comment after first use.
            MainDatabaseProcess();

            // Get information about some student by id from 1 to 5.
            Console.Write($"Insert student Id: ");
            var studentId = int.Parse(Console.ReadLine());
            ReadData(studentId);
        }

        private static void ReadData(int studentId)
        {            
            try
            {
                using (var dbContext = new StudentSystemContext())
                {
                    var student = dbContext.Students
                        .FirstOrDefault(s => s.StudentId == studentId);

                    Console.WriteLine($"Full Name: {student.Name}");
                    Console.WriteLine($"Register Date: {student.RegisteredOn.Date}");    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void MainDatabaseProcess()
        {
            try
            {
                using (var dbContext = new StudentSystemContext())
                {
                    // For creating database.
                    CreateDatabaase(dbContext);

                    // For deleting database.
                    //DeleteDatabase(dbContext);

                    // For initialize database.
                    //InitializeCreateMigration(dbContext);

                    // For seed hardcode data.
                    SeedData(dbContext);

                    // For drop database, initilize migration and seed data.
                    //ResetDatabase();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ResetDatabase()
        {
            DatabaseInitializer.ResetDatabase();
        }

        private static void SeedData(StudentSystemContext dbContext)
        {
            DatabaseInitializer.InitialSeed(dbContext);
        }

        private static void InitializeCreateMigration(StudentSystemContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        private static void DeleteDatabase(StudentSystemContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
        }

        private static void CreateDatabaase(StudentSystemContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
