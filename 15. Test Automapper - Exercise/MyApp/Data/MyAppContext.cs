namespace MyApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MyAppContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public MyAppContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
