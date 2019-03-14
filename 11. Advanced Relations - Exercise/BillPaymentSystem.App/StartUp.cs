namespace BillPaymentSystem.App
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using BillPaymentSystem.App.Core.Services.Contracts;
    using BillPaymentSystem.Data;
    using Core;
    using Core.Contracts;
    using BillPaymentSystem.App.Core.Services;

    public class StartUp
    {
        public static void Main()
        {
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
    }
}
