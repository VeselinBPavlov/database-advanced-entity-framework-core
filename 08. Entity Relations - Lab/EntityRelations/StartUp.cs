namespace EntityRelations
{
    using System;
    
    public class StartUp
    {
        public static void Main()
        {
            try
            {
                using (var dbContext = new StudentContext())
                {
                    dbContext.Database.EnsureCreated();
                    Console.WriteLine("Database created!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
