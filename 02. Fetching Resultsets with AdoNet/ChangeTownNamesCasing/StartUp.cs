namespace ChangeTownNamesCasing
{
    using HelperClasses.Configurations;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        static void Main()
        {
            string country = Console.ReadLine();
            int rowsAffected = 0;
            IList<string> towns = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(DbCommand.ChangeTownName, connection))
                    {
                        command.Parameters.AddWithValue("@countryName", country);
                        rowsAffected = command.ExecuteNonQuery();
                        
                    }

                    using (SqlCommand command = new SqlCommand(DbCommand.SelectTownsByCountry, connection))
                    {
                        command.Parameters.AddWithValue("@countryName", country);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                towns.Add((string)reader[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (towns.Count != 0)
            {
                Console.WriteLine(string.Format(Util.UpdateTownsSuccess, rowsAffected));
                Console.WriteLine($"[{string.Join(", ", towns)}]");
            }
            else
            {
                Console.WriteLine(Util.NoAffectedTowns);
            }
        }
    }
}
