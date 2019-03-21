namespace MyApp.Core.Commands
{
    using System;
    using System.Globalization;
    using System.Linq;

    using AutoMapper;
    using Contracts;
    using MyApp.Data;
    using MyApp.Core.ViewModels;

    public class SetBirthdayCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetBirthdayCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            DateTime birthDay = DateTime.ParseExact(inputArgs[1], "dd-MM-yyyy", CultureInfo.InstalledUICulture);

            var employee = this.context.Employees.FirstOrDefault(x => x.Id == employeeId);

            employee.BirthDay = birthDay;

            this.context.Update(employee);
            this.context.SaveChanges();

            var employeeDto = mapper.CreateMappedObject<EmployeeBirthdayDto>(employee);

            string result = $"Birthday of {employeeDto.FirstName} {employeeDto.LastName} setted up to {employeeDto.BirthDay.ToString("dd-MM-yyyy")}!";

            return result;
        }
    }
}
