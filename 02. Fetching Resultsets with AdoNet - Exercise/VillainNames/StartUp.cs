namespace VillainNames
{
    using HelperClasses;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            List<Tuple<string, int>> villains = new List<Tuple<string, int>>();

            // DB operations.
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(DbCommand.VillainNames, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                villains.Add(new Tuple<string, int>((string)reader["Name"], (int)reader["MinionsCount"]));
                                Console.WriteLine(Util.ReadingDataSuccess);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Print.
            foreach (var villain in villains)
            {
                Console.WriteLine($"{villain.Item1} - {villain.Item2}");
            }
        }
    }
}