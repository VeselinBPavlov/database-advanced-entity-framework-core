namespace CodeFirst
{
    using EFCodeFirst.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            LoggerFactory SqlCommandLoggerFactory
               = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true) });

            DbContextOptionsBuilder<BlogDbContext> optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();

            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\BlogDb_Data.mdf;Database=BlogDb;Integrated Security=true;", s => s.MigrationsAssembly("EFCodeFirst.Infrastructure"))
                .UseLoggerFactory(SqlCommandLoggerFactory)
                .EnableSensitiveDataLogging();
        }
    }
}
