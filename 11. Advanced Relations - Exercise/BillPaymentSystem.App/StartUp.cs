namespace BillPaymentSystem.App
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Core.Services.Contracts;
    using BillPaymentSystem.Data;
    using Core;
    using Core.Contracts;
    using Core.Services;
    using BankPaymentSystem.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            // Create database and seed data on first strat.
            // ConfigureDatabaase();

            IServiceProvider serviceProvider = ConfigureServices();

            ICommandInterpreter commandInterpreter = new CommandInterpreter(serviceProvider);
            IEngine engine = new Engine(commandInterpreter);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<BillPaymentSystemContext>(options =>
            {
                options.UseSqlServer(Configuration.ConnectionString, s => s.MigrationsAssembly("BillPaymentSystem.Data"));
            });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBankService, BankService>();

            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }

        private static void ConfigureDatabaase()
        {
            // Change connection string in Configure file.
            try
            {
                using (BillPaymentSystemContext context = new BillPaymentSystemContext())
                {
                    context.Database.EnsureCreated();
                    Console.WriteLine("Database created!");

                    Initializer.Seed(context);
                    Console.WriteLine("Data insert success!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }        
    }
}
