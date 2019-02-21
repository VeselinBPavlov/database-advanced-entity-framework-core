namespace AdoFirstSteps
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            const string connectionString = 
                @"Server=(localdb)\MyInstance;
                Database=SoftUni;
                Integrated Security=true;";

            SqlCommand command;
            int employeeCount = 0;
            List<Employee> employees = new List<Employee>();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // When we open connection with using, connection will be closed automaticly.
            using (connection)
            {
                command = new SqlCommand("SELECT COUNT(*) FROM Employees", connection);
                employeeCount = (int)command.ExecuteScalar();                
            }

            connection = new SqlConnection(connectionString);
            connection.Open();
            using (connection)
            {
                command = new SqlCommand("SELECT TOP(10) * FROM Employees ORDER BY Salary DESC", connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = (string)reader["FirstName"];
                        employee.LastName = (string)reader["LastName"];
                        employee.Salary = (decimal)reader["Salary"];
                        employees.Add(employee);
                    }
                }
            }

            Console.WriteLine($"Employees count: {employeeCount}");
            employees.ForEach(x => Console.WriteLine($"{x.FirstName} {x.LastName} - {x.Salary:f2}"));
        }
    }
}

