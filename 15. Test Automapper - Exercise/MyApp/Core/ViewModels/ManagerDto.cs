namespace MyApp.Core.ViewModels
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<EmployeeAddDto> ManagedEmployees { get; set; }

        public ManagerDto()
        {
            this.ManagedEmployees = new List<EmployeeAddDto>();
        }
    }
}
