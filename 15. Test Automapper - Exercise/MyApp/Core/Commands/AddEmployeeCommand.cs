namespace MyApp.Core.Commands
{
    using Contracts;
    using MyApp.Models;
    using MyApp.Data;
    using AutoMapper;
    using MyApp.Core.ViewModels;

    public class AddEmployeeCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public AddEmployeeCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            string firstName = inputArgs[0];
            string lastName = inputArgs[1];
            decimal salary = decimal.Parse(inputArgs[2]);

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            this.context.Employees.Add(employee);
            this.context.SaveChanges();

            var employeeDto = mapper.CreateMappedObject<EmployeeAddDto>(employee);

            string result = $"Registered successfully: {employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary}!";

            return result;            
        }
    }
}
