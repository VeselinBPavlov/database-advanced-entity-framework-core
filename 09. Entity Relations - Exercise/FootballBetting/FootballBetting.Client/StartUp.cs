namespace P03_FootballBetting
{
    using System;

    using Data;

    public class StartUp
    {
        public static void Main()
        {
            try
            {
                using (var db = new FootballBettingContext())
                {
                    db.Database.EnsureCreated();
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