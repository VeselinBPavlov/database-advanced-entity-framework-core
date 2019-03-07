namespace P01_StudentSystem.Data.Initializer
{
    using Microsoft.EntityFrameworkCore;
    using DataGenerators;

    public class DatabaseInitializer
    {
        public static void ResetDatabase()
        {
            using (var context = new StudentSystemContext())
            {
                context.Database.EnsureDeleted();

                context.Database.Migrate();

                InitialSeed(context);
            }            
        }

        public static void InitialSeed(StudentSystemContext context)
        {
            SeedStudents(context);

            SeedCourses(context);

            SeedResources(context);

            SeedHomeworkSubmissions(context);
        }

        private static void SeedStudents(StudentSystemContext context)
        {
            StudentGenerator.InitialStudentsSeed(context);
        }

        private static void SeedCourses(StudentSystemContext context)
        {
            CourseGenerator.InitialCourseSeed(context);
        }

        private static void SeedResources(StudentSystemContext context)
        {
            ResourceGenerator.InitialResourceSeed(context);
        }

        private static void SeedHomeworkSubmissions(StudentSystemContext context)
        {
            HomeworkSubmissionsGenerator.InitialHomeworkSubmissionSeed(context);
        }
    }    
}
