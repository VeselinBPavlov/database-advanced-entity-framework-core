namespace MiniORM.App
{
    using Data;
    using Data.Entities;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var connectionString = @"Server=(localdb)\MyInstance;Database=MiniORM;Integrated security=True";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirstName = "Gancho",
                LastName = "Ganchov",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            var employee = context.Employees.Last();
            employee.FirstName = "Hello";

            context.SaveChanges();
        }
    }
}
