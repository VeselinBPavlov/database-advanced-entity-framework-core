namespace DatabaseFirst
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;

    using Data;
    using System.Linq;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            // Scaffold-DbContext "Server=(localdb)\MyInstance;Database=SoftUni;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data

            // 1. Change connection string.
            // 2. Write command in the Package Manager Console.
            // 3. Classes will be plural (rename on hand).
            // 4. Relations and constraints will be descriped with fluent api. 
            // 5. If you want data annotations add -DataAnnotations in the end of the command.

            // Use logger for reading sql queries on console.
            LoggerFactory SqlCommandLoggerFactory 
                = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)});

            DbContextOptionsBuilder<SoftUniContext> optionsBuilder = new DbContextOptionsBuilder<SoftUniContext>();

            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MyInstance;Database=SoftUni;Integrated Security=true;")
                .UseLoggerFactory(SqlCommandLoggerFactory);

            // Test only single commands.
            // Change entity.
            using (var context = new SoftUniContext(optionsBuilder.Options))
            {
                var employee = context.Employees
                    .Include(e => e.Department)
                    .FirstOrDefault();

                employee.FirstName = "Pesho";
                employee.LastName = "Ivanov";

                context.SaveChanges();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} department.");
            }

            // Insert single entity.
            using (var context = new SoftUniContext(optionsBuilder.Options))
            {
                var project = new Project
                {
                    Name = "EFCore",
                    Description = "EF Core Demo",
                    StartDate = DateTime.Now
                };

                context.Projects.Add(project);

                context.SaveChanges();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"{project.Name} project added to the database.");
            }

            // Insert with relation.
            using (var context = new SoftUniContext(optionsBuilder.Options))
            {
                var address = new Address
                {
                    AddressText = "Balkan 4",
                    Town = new Town
                    {
                        Name = "Dinevo"
                    }
                };

                context.Addresses.Add(address);

                context.SaveChanges();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"New address {address.AddressText} added in new city of {address.Town.Name}");
            }

            // Remove entity with reference.
            using (var context = new SoftUniContext(optionsBuilder.Options))
            {
                var town = context.Towns
                    .Include(t => t.Addresses)
                    .FirstOrDefault(t => t.Name == "Dinevo");

                context.RemoveRange(town.Addresses);
                context.Towns.Remove(town);

                context.SaveChanges();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"{town.Name} city remove from database.");
            }
        }
    }
}