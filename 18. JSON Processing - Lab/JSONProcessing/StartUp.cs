namespace JSONProcessing
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            var person = new Person()
            {
                FirstName = "Pesho",
                LastName = "Peshev",
                Age = 25,
                Interests = new List<string>()
                {
                    "Programming",
                    "Basketball",
                    "Movies"
                }            
            };

            // Serialize and deserialize with custom serializer.
            string personSerialized = JsonHelper.SelializeObject(person);

            Console.WriteLine(personSerialized);

            var personDeserialized = JsonHelper.DeserializeObject<Person>(personSerialized);

            Console.WriteLine($"{personDeserialized.FirstName}" +
                $" {personDeserialized.LastName}, Age: {personDeserialized.Age}" +
                $"{Environment.NewLine}{string.Join(Environment.NewLine, personDeserialized.Interests)}");

            Console.WriteLine();

            // Serialize and deserialize with Newtonsoft.Json
            string personSerNewton = JsonConvert.SerializeObject(person, Formatting.Indented); // Beutify with Idented

            Console.WriteLine(personSerNewton);

            var personDesNewton = JsonConvert.DeserializeObject<Person>(personSerNewton);

            Console.WriteLine($"{personDesNewton.FirstName}" +
                $" {personDesNewton.LastName}, Age: {personDesNewton.Age}" +
                $"{Environment.NewLine}{string.Join(Environment.NewLine, personDesNewton.Interests)}");

            // Deserilize anonymous types.
            var json = @"{'firstName': 'Gosho',
                          'lastName': 'Ivanov',
                          'jobTitle': 'Manager'}";

            var template = new
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                JobTitle = string.Empty
            };

            var personAnonymous = JsonConvert.DeserializeAnonymousType(json, template);

            Console.WriteLine($"{personAnonymous.FirstName} {personAnonymous.LastName} - {personAnonymous.JobTitle}");


        }
    }
}
