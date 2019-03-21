namespace MyApp
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    using Core;
    using Core.Contracts;
    using Data;

    public class StartUp
    {
        public static void Main()
        {
            IServiceProvider serviceProvider = ConfigureServices();
                        
            IEngine engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<MyAppContext>(options =>
            {
                options.UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\MyApp_Data.mdf;Database=MyApp;Integrated Security=true;");
            });

            services.AddTransient<ICommandInterpreter, CommandInterpreter>();

            services.AddTransient<Mapper>();

            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
