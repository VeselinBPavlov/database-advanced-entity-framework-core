namespace P01_StudentSystem.Data.Initializer.DataGenerators
{
    using P01_StudentSystem.Data.Models;
    using System;
    
    public class ResourceGenerator
    {
        internal static void InitialResourceSeed(StudentSystemContext context)
        {
            Random random = new Random();

            var resourceNames = new string[]
            {
                "Type of Managers",
                "Human Resources",
                "How to create teams",
                "The future of entrepreneurship",
                "Management of 21 century"
            };

            for (int i = 0; i < resourceNames.Length; i++)
            {
                context.Resources.Add(new Resource()
                {
                    Name = resourceNames[i],
                    Url = "managers.com",
                    ResourceType = Enum.Parse<ResourceType>(random.Next(1, 4).ToString()),
                    CourseId = random.Next(1, 5)
                });

                context.SaveChanges();
            }
        }

        public static void Generate(string resourceName, string url, ResourceType resourceType, int courseId, StudentSystemContext context)
        {
            context.Resources.Add(new Resource()
            {
                Name = resourceName,
                Url = url,
                ResourceType = resourceType,
                CourseId = courseId
            });
        }
    }
}
