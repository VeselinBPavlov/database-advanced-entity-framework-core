namespace IncreaseMinionAge
{
    using HelperClasses;
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
            string[] sqlVariables;
            dynamic[] entityData;

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
                {
                    connection.Open();

                    for (int i = 0; i < ids.Length; i++)
                    {
                        sqlVariables = new string[] { "@Id" };
                        entityData = new dynamic[] { ids[i] };

                        Service<int>.ExecNonQuery(connection, DbCommand.MinionIncreaseAge, sqlVariables, entityData);   
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
