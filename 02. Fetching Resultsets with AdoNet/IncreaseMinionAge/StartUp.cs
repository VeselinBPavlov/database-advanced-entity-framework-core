namespace IncreaseMinionAge
{
    using HelperClasses.Configurations;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            int[] ids = Console.ReadLine().Split().Select(int.Parse).ToArray();
            List<Tuple<string, int>> minions = new List<Tuple<string, int>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();

                    for (int i = 0; i < ids.Length; i++)
                    {
                        using (SqlCommand command = new SqlCommand(DbCommand.MinionIncreaseAge, connection))
                        {
                            command.Parameters.AddWithValue("@Id", ids[i]);
                            command.ExecuteNonQuery();
                        }
                    }

                    using (SqlCommand command = new SqlCommand(DbCommand.SelectMinionNameAge, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                minions.Add(new Tuple<string, int>((string)reader[0], (int)reader[1]));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }           

            minions.ForEach(x => Console.WriteLine($"{x.Item1} {x.Item2}"));
        }
    }
}
