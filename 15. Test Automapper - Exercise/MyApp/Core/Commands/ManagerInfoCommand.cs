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
    
    public class ManagerInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ManagerInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int managerId = int.Parse(inputArgs[0]);

            var manager = this.context.Employees
                .Include(m => m.ManagedEmployees)
                .FirstOrDefault(m => m.Id == managerId);

            if (manager == null)
            {
                throw new ArgumentNullException($"Manager with ID {managerId} does not exists!");
            }

            if (manager.ManagedEmployees.Count == 0)
            {
                throw new ArgumentException($"Employee with ID {managerId} is not a manager!");
            }

            var managerDto = this.mapper.CreateMappedObject<ManagerDto>(manager);

            var sb = new StringBuilder();

            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.ManagedEmployees.Count}");

            foreach (var employee in managerDto.ManagedEmployees)
            {
                sb.AppendLine($"    - {employee.FirstName} {employee.LastName} - ${employee.Salary}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
