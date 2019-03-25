namespace CSharpAutoMappingObjects
{
    using System;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using Newtonsoft.Json;
    using AutoMapper.QueryableExtensions;

    using Models;
    using DTOs;
    using AutoMapper.EquivalencyExpression;
    using CSharpAutoMappingObjects.Collections;
    using System.Collections.Generic;
    using AutoMapper.EntityFrameworkCore;
    using CSharpAutoMappingObjects.Profiles;

    public class StartUp
    {
        public static void Main()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddDbContext<SoftUniContext>(options =>
                {
                    options.UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\SoftUni_Data.mdf;Database=SoftUni;Integrated Security=true;")
                        .EnableSensitiveDataLogging();
                });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = serviceProvider.GetService<SoftUniContext>();

            // Initialize mapping objects.
            Mapper.Initialize(cfg =>
            {
                // Add profile.
                cfg.AddProfile<EmployeeProfile>();
                // Configure object property.
                cfg.CreateMap<Department, DepartmentDTO>();
                // Configure object collections.
                cfg.AddCollectionMappers();
                cfg.CreateMap<OrderDTO, Order>()
                    .EqualityComparison((odto, o) => odto.Id == o.Id);
                // Mapping collections from database.
                cfg.SetGeneratePropertyMaps<GenerateEntityFrameworkCorePrimaryKeyPropertyMaps<SoftUniContext>>();
            });
            // Hand mapping.
            //.ForMember(dt => dt.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")));
            // Reverse mapping.
            //.ReverseMap());

            // Find object.
            var employee = context.Employees.FirstOrDefault();

            // Map objects.
            var employeeDto = Mapper.Map<EmployeeDTO>(employee);

            // With QueryableExtensions we load object properties.
            // ProjectTo modify expression tree without loading all data for nested objects.
            var emp = context.Employees
                .ProjectTo<EmployeeDTO>()
                .FirstOrDefault();

            // We can map collections.
            var emps = context.Employees
                .Where(e => e.Department.Name == "Sales")
                .ProjectTo<EmployeeDTO>()
                .ToList();

            // Convert JSON and print object.
            //Console.WriteLine(JsonConvert.SerializeObject(emp));
                      
            // Mapping collections (no database).
            var ordersDto = new List<OrderDTO>()
            {
                new OrderDTO { Id = 1, ProductName = "Shoes" },
                new OrderDTO { Id = 2, ProductName = "Hat" }
            };

            var orders = new List<Order>()
            {
                new Order { Id = 1, ProductName = "Carrot", Price = 10 },
                new Order { Id = 2, ProductName = "Tomato", Price = 5 }
            };

            var ordersDTOtoOrders = Mapper.Map(ordersDto, orders);
            var ordersToOrdersDTO = Mapper.Map(orders, ordersDto);

            //Console.WriteLine(JsonConvert.SerializeObject(ordersDTOtoOrders));
            //Console.WriteLine(JsonConvert.SerializeObject(ordersToOrdersDTO));

            // Mapping collections from database. Throw error.
            //emp.LastName = "Petrov";
            //context.Employees.Persist().InsertOrUpdate(emp);
            //context.SaveChanges();
        }
    }
}
