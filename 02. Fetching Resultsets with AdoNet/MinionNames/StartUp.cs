namespace MinionNames
{
    using HelperClasses.Configurations;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            string villainName = string.Empty;
            Dictionary<string, int> minions = new Dictionary<string, int>();

            int id = int.Parse(Console.ReadLine());

            // DB operations.
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(DbCommand.MinionNamesVillainName, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        villainName = (string)command.ExecuteScalar();

                        if (villainName == null)
                        {
                            Console.WriteLine($"No villain with ID {id} exists in the database.");
                            return;
                        }
                    }

                    using (SqlCommand command = new SqlCommand(DbCommand.MinionNames, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {                                
                                minions.Add((string)reader[1], (int)reader[2]);
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
            Console.WriteLine($"Villain: {villainName}");

            if (minions.Count != 0)
            {
                var rowNumber = 0;
                foreach (var minion in minions)
                {        
                    Console.WriteLine($"{++rowNumber}. {minion.Key} - {minion.Value}");
                }
            }
            else
            {
                Console.WriteLine("(no minions)");
            }
        }
    }
}
