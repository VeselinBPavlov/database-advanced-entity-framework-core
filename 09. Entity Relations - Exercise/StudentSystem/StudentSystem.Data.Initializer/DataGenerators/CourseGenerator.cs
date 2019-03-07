namespace P01_StudentSystem.Data.Initializer.DataGenerators
{    
    using P01_StudentSystem.Data;
    using P01_StudentSystem.Data.Models;
    using System;

    public class CourseGenerator
    {
        internal static void InitialCourseSeed(StudentSystemContext context)
        {
            Random random = new Random();

            var courseNames = new string[]
            {
                "Basics of Management",
                "Management Skills",
                "Logistics",
                "Team Management",
                "Industrial Relationships"
            };

            for (int i = 0; i < courseNames.Length; i++)
            {
                context.Courses.Add(new Course()
                {
                    Name = courseNames[i],
                    StartDate = DateTime.Now.AddDays(random.Next(1, 10)),
                    EndDate = DateTime.Now.AddDays(random.Next(10, 20)),
                    Price = 1 + ((decimal)random.NextDouble() * (random.Next(2, 100) - 1))
                });

                context.SaveChanges();
            }
        }

        public static void Generate(string courseName, DateTime startDate, DateTime endDate, StudentSystemContext context)
        {
            context.Courses.Add(new Course()
            {
                Name = courseName,
                StartDate = startDate,
                EndDate = endDate
            });
        }
    }
}
