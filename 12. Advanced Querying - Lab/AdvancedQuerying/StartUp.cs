namespace AdvancedQuerying
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using Z.EntityFramework.Plus;

    public class StartUp
    {
        public static void Main()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddLogging(configure => configure.AddDebug())
                .AddDbContext<AdvancedDbContext>(options =>
                {
                    options.UseSqlServer(@"Server=(localdb)\MyInstance;AttachDbFilename=D:\Courses\Data\AdvancedQueryingDemo_Data.mdf;Database=AdvancedQueryingDemo;Integrated Security=true;")
                        .EnableSensitiveDataLogging();
                });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = serviceProvider.GetService<AdvancedDbContext>();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            //With ready query is a risk from SQL injection.
            string query = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";

            //FromSql query is parametrized.
            bool result = context.Users.FromSql($"SELECT * FROM Users WHERE Username = {username} AND Password = {password}").Count() > 0;

            if (result)
            {
                Console.WriteLine("You are in!");
            }
            else
            {
                Console.WriteLine("Invalid username or password!");
            }

            // Detached object.Can not be saved in database.
            var user = context.Users.AsNoTracking().FirstOrDefault();
            user.Username = "Pesho";
            context.SaveChanges();

            user = context.Users.FirstOrDefault();

            var userEntry = context.Entry(user);
            // Use entry state to attach or detach entity.
            userEntry.State = EntityState.Detached;
            user.Username = "Pesho";
            context.SaveChanges();

            // With Z.EntityFramework.Plus.EfCore we can make a bulk edit/delete of entities.
            // This state will delete/update entities directly from database.
            var users = context.Users
                .Where(t => t.Username == "Nasko")
                .Update(u => new User() { Username = "Plamen" });


            users = context.Users
                .Where(u => u.Username == "Pesho")
                .Delete();

            // Eager loading.
            context.Entry(user)
                .Reference(u => u.Town)
                .Load();

            var town = context.Towns.FirstOrDefault();

            context.Entry(town)
                .Collection(t => t.Users)
                .Load();

            // With Include and ThenInclude can load referece properties.
            context.Towns.Include(t => t.Users);

            // For Lazy loading install package and set up from OnCofiguring method.
        }
    }
}
