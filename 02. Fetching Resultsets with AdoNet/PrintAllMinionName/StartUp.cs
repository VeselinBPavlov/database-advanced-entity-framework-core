namespace PrintAllMinionName
{
    using HelperClasses.Configurations;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            IList<string> minionNames = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(DbCommand.PrintAllNames, connection))
                    {
                        using  (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                minionNames.Add((string)reader[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            for (int i = 0; i < minionNames.Count / 2; i++)
            {
                Console.WriteLine(minionNames[i]);
                Console.WriteLine(minionNames[minionNames.Count - 1 - i]);
            }
           
            if (minionNames.Count % 2 != 0)
            {
                Console.WriteLine(minionNames[minionNames.Count / 2]);
            }
        }
    }
}
