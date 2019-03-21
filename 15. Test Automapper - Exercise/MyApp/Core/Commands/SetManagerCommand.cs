namespace MyApp.Core.Commands
{
    using AutoMapper;
    using Contracts;
    using MyApp.Core.ViewModels;
    using MyApp.Data;
    using System;

    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetManagerCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            int managerId = int.Parse(inputArgs[1]);

            var employee = this.context.Employees.Find(employeeId);
            var manager = this.context.Employees.Find(managerId);

            if (employee == null)
            {
                throw new ArgumentNullException($"Employee with ID {employeeId} does not exists!");
            }

            if (manager == null)
            {
                throw new ArgumentNullException($"Manager with ID {managerId} does not exists!");
            }

            employee.Manager = manager;

            this.context.SaveChanges();

            var employeeDto = mapper.CreateMappedObject<EmployeeAddDto>(employee);
            var managerDto = mapper.CreateMappedObject<ManagerDto>(manager);

            return $"Employee {employeeDto.FirstName} {employeeDto.LastName} now has for manager {managerDto.FirstName} {managerDto.LastName}!";
        }
    }
}
