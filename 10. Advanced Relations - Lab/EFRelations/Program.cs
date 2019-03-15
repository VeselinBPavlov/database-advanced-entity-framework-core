using EFRelations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EFRelations
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddLogging(configure => configure.AddConsole())
                .AddDbContext<StudentSystemContext>(options => 
                {
                    options.UseSqlServer("Server=localhost,8976;Database=StudentSystem;User=adotest;Password=1234",
                        s => s.MigrationsAssembly("EFRelations.Infrastructure"))
                        .EnableSensitiveDataLogging();
                });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = serviceProvider.GetService<StudentSystemContext>();

            var students = context.Students.ToList();
        }
    }
}
