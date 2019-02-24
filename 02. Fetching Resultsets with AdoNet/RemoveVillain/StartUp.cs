namespace RemoveVillain
{
    using HelperClasses.Configurations;
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());
            string villainName = string.Empty;
            int releasedMinions = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(DbCommand.SelectVillainById, connection))
                    {
                        command.Parameters.AddWithValue("@villainId", villainId);
                        villainName = (string)command.ExecuteScalar();

                        if (villainName == null)
                        {
                            Console.WriteLine(Util.NoViillainFound);
                            return;
                        }
                    }

                    using (SqlCommand command = new SqlCommand(DbCommand.DeleteMinionVllainById, connection))
                    {
                        command.Parameters.AddWithValue("@villainId", villainId);
                        releasedMinions = command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(DbCommand.DeleteVillainById, connection))
                    {
                        command.Parameters.AddWithValue("@villainId", villainId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(Util.DeletedVillain, villainName);
            Console.WriteLine(Util.ReleasedMinions, releasedMinions);
        }
    }
}
