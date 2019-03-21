namespace MyApp.Core.Commands
{
    using System.Linq;

    using AutoMapper;
    using Contracts;
    using MyApp.Data;
    using MyApp.Core.ViewModels;

    public class SetAddressCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetAddressCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            string address =inputArgs[1];

            var employee = this.context.Employees.FirstOrDefault(x => x.Id == employeeId);

            employee.Address = address;

            this.context.Update(employee);
            this.context.SaveChanges();

            var employeeDto = mapper.CreateMappedObject<EmployeeAddressDto>(employee);

            string result = $"Address of {employeeDto.FirstName} {employeeDto.LastName} setted up to {employeeDto.Address}!";

            return result;
        }
    }
}
