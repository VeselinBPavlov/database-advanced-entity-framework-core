namespace SoftUni
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Models;

    public class StartUp
    {
        public static void Main()
        {
            try
            {
                using (SoftUniContext context = new SoftUniContext())
                {
                    Console.WriteLine(/*Call methohod here.*/);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // 03. Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.EmployeeId).ToArray();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        // 04. Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName).ToArray();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        // 05. Employees from Research and Development 
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToArray();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        // 06. Adding a New Address and Updating Employee 
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;

            context.SaveChanges();

            var employees = context.Employees
                .Select(e => new
                {
                    e.AddressId,
                    e.Address.AddressText
                })
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .ToArray();

            foreach (var e in employees)
            {
                sb.AppendLine(e.AddressText);
            }

            return sb.ToString().TrimEnd();
        }

        // 07. Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employeesProjects = context.Employees
                .Where(p => p.EmployeesProjects.Any(s => s.Project.StartDate.Year >= 2001 && s.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    EmployeeName = $"{e.FirstName} {e.LastName}",
                    ManagerName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        StartDate = p.Project.StartDate,
                        EndDate = p.Project.EndDate
                    }).ToList()
                })
                .Take(10)
                .ToList();

            foreach (var ep in employeesProjects)
            {
                sb.AppendLine($"{ep.EmployeeName} - Manager: {ep.ManagerName}");

                foreach (var p in ep.Projects)
                {
                    string startDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt");
                    string endDate;

                    if (p.EndDate.HasValue)
                    {
                        endDate = p.EndDate?.ToString("M/d/yyyy h:mm:ss tt");
                    }
                    else
                    {
                        endDate = "not finished";
                    }

                    sb.AppendLine($"--{p.ProjectName} - {startDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        // 08. Addresses by Town 
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var addresses = context.Addresses
                .Select(a => new
                {
                    EmployeesCount = a.Employees.Count(),
                    AddressText = a.AddressText,
                    TownName = a.Town.Name
                })
                .OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        // 09. Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employee = context.Employees
                .Select(e => new
                {
                    FullName = $"{e.FirstName} {e.LastName}",
                    EmployeeId = e.EmployeeId,
                    JobTitle = e.JobTitle,
                    Projects = context.EmployeesProjects.Select(p => new
                    {
                        EmpId = p.EmployeeId,
                        ProjectName = p.Project.Name
                    })
                    .Where(p => p.EmpId == e.EmployeeId)
                    .OrderBy(p => p.ProjectName)
                    .ToList()
                })
                .FirstOrDefault(e => e.EmployeeId == 147);

            sb.AppendLine($"{employee.FullName} - {employee.JobTitle}");

            foreach (var p in employee.Projects)
            {
                sb.AppendLine(p.ProjectName);
            }

            return sb.ToString().TrimEnd();
        }

        // 10. Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .Select(d => new
                {
                    EmployeeCount = d.Employees.Count,
                    DepartmentName = d.Name,
                    ManagerFullName = $"{d.Manager.FirstName} {d.Manager.LastName}",
                    Employees = d.Employees
                        .Select(e => new
                        {
                            e.FirstName,
                            e.LastName,
                            e.JobTitle
                        })
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .ToList()
                })
                .OrderBy(d => d.EmployeeCount)
                .ThenBy(d => d.DepartmentName)
                .ToList();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.DepartmentName} - {d.ManagerFullName}");

                foreach (var e in d.Employees)
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        // 11. Find Latest 10 Projects 
        public static string GetLatestProjects(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var projects = context.Projects
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
                sb.AppendLine(p.Description);
                sb.AppendLine(p.StartDate.ToString("M/d/yyyy h:mm:ss tt"));
            }

            return sb.ToString().TrimEnd();
        }

        // 12. Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var departments = new string[]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employees = context.Employees
                .Where(e => departments.Contains(e.Department.Name))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary * 1.12m:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        // 13. Find Employees by First Name Starting With "Sa"
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => EF.Functions.Like(e.FirstName, "sa%"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        // 14. Delete Project by Id 
        public static string DeleteProjectById(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employeesProject = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2);

            context.RemoveRange(employeesProject);

            var project = context.Projects
                .FirstOrDefault(p => p.ProjectId == 2);

            context.Remove(project);
            context.SaveChanges();

            var projects = context.Projects
                .Take(10)
                .ToArray();

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
            }

            return sb.ToString().TrimEnd();
        }

        //15. Remove Town
        public static string RemoveTown(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var town = context.Towns
                .FirstOrDefault(t => t.Name == "Seattle");

            var addresses = town.Addresses.ToList();
            var addressesCount = addresses.Count();

            foreach (var address in addresses)
            {
                foreach (var employee in address.Employees)
                {
                    employee.AddressId = null;
                }
            }

            context.RemoveRange(addresses);
            context.Remove(town);
            context.SaveChanges();

            sb.AppendLine($"{addressesCount} addresses in Seattle were deleted");
            
            return sb.ToString().TrimEnd();
        }
    }
}
