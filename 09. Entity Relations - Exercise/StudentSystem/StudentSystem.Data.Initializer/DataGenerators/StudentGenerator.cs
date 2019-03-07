namespace P01_StudentSystem.Data.Initializer.DataGenerators
{
    using P01_StudentSystem.Data.Models;
    using System;

    public class StudentGenerator
    {
        internal static void InitialStudentsSeed(StudentSystemContext context)
        {
            Random random = new Random();

            var studentNames = new string[]
            {
                "Pesho Peshev",
                "Gosho Goshev",
                "Tosho Toshev",
                "Misho Mishev",
                "Sasho Sashev"
            };

            for (int i = 0; i < studentNames.Length; i++)
            {
                context.Students.Add(new Student()
                {
                    Name = studentNames[i],
                    RegisteredOn = DateTime.Now
                });

                context.SaveChanges();
            }
        }

        public static void Generate(string studentName, DateTime registeredOn, StudentSystemContext context)
        {
            context.Students.Add(new Student()
            {
                Name = studentName,
                RegisteredOn = registeredOn
            });
        }
    }
}
