namespace MyApp.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using Contracts;
    using MyApp.Data;
    using MyApp.Core.ViewModels;

    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeePersonalInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);

            var employee = this.context.Employees.FirstOrDefault(x => x.Id == employeeId);

            var employeeDto = mapper.CreateMappedObject<EmployeeInfoDto>(employee);

            var sb = new StringBuilder();
            sb.AppendLine($"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary}");

            if (employeeDto.BirthDay != null)
            {
                sb.AppendLine($"Birthday: {employeeDto.BirthDay.Value.ToString("dd-MM-yyyy")}");
            }

            if (employeeDto.Address != null)
            {
                sb.AppendLine($"Address: {employeeDto.Address}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
