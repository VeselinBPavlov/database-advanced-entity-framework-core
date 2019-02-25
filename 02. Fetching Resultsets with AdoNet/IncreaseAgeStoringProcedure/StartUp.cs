namespace IncreaseAgeStoringProcedure
{
    using System;
    using System.Data.SqlClient;
    using HelperClasses;

    public class StartUp
    {
        public static void Main()
        {
            int id = int.Parse(Console.ReadLine());
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
                {
                    connection.Open();

                    // Before using of procedure, create it in the database.
                    using (SqlCommand command = new SqlCommand(DbCommand.ExecuteIncreaseAgeProcedure, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            Console.WriteLine($"{(string)reader[0]} - {(int)reader[1]} years old");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }
    }
}
