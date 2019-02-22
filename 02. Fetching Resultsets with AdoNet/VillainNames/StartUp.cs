namespace VillainNames
{
    using HelperClasses.Configurations;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            Dictionary<string, int> villains = new Dictionary<string, int>();

            // DB operations.
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(DbCommand.VillainNames, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                villains.Add((string)reader["Name"], (int)reader["MinionsCount"]);
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
                Console.WriteLine($"{villain.Key} - {villain.Value}");
            }
        }
    }
}