namespace MyApp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using MyApp.Core.ViewModels;
    using MyApp.Data;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ListEmployeesOlderThanCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var age = int.Parse(inputArgs[0]);

            var employees = context.Employees
                .Include(e => e.Manager)
                .Where(e => EF.Functions.DateDiffYear(e.BirthDay, DateTime.Now) > age)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                var employeeDto = mapper.CreateMappedObject<EmployeeAgeDto>(employee);
                var manager = employee.Manager != null ? employee.Manager.LastName : "[no manager]";

                sb.AppendLine($"{employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary} - Manager: {manager}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
