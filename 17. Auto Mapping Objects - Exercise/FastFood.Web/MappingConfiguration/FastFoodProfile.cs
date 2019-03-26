namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;

    using ViewModels.Employees;
    using Models;

    using ViewModels.Positions;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            // Employees
            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionName, y => y.MapFrom(p => p.Name));

            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position, y => y.MapFrom(s => s.Position.Name));

            // Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.CategoryName));

            this.CreateMap<Category, CategoryAllViewModel>();

            // Items
            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(c => c.Name));

            this.CreateMap<CreateItemInputModel, Item>();

            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(i => i.Category.Name));

            // Orders.
            this.CreateMap<CreateOrderInputModel, Order>();

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.OrderId, y => y.MapFrom(o => o.Id))
                .ForMember(x => x.Employee, y => y.MapFrom(o => o.Employee.Name))
                .ForMember(x => x.DateTime, y => y.MapFrom(o => o.DateTime.ToString("g")));
        }
    }
}
