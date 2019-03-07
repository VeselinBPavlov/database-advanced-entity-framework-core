namespace P01_StudentSystem.Data.Initializer.DataGenerators
{
    using Models;
    using System;

    public class HomeworkSubmissionsGenerator
    {
        internal static void InitialHomeworkSubmissionSeed(StudentSystemContext context)
        {
            Random random = new Random();

            var contents = new string[]
            {
                "Management functions",
                "Group conflicts",
                "Sustanable development",
                "Brain storming",
                "Мanagement structures"
            };

            for (int i = 0; i < contents.Length; i++)
            {
                context.HomeworkSubmissions.Add(new Homework()
                {
                    Content = contents[i],
                    ContentType = Enum.Parse<ContentType>(random.Next(1, 3).ToString()),
                    SubmissionTime = DateTime.Now.AddDays(7),
                    StudentId = random.Next(1, 5),
                    CourseId = random.Next(1, 5)
            });

                context.SaveChanges();
            }
        }

        public static void Generate(string content, string contentType, DateTime submissionTime, int studentId, int courseId, StudentSystemContext context)
        {
            context.HomeworkSubmissions.Add(new Homework()
            {
                Content = content,
                ContentType = Enum.Parse<ContentType>(contentType),
                SubmissionTime = submissionTime,
                StudentId = studentId,
                CourseId = courseId
            });
        }
    }
}
