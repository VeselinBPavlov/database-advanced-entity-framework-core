namespace InitialSetup
{
    using HelperClasses.Configurations;
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            // Create instance for string[].
            DbCommand dbCommand = new DbCommand();

            // DB operations.
            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationStringBeforeCreateDB))
                {
                    // Create database.
                    connection.Open();
                    ExecNonQuery(connection, DbCommand.InitialSetupCreateDB);
                    Console.WriteLine(Util.DBCreateSuccess);                    
                }

                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();
                    // Create tables.
                    foreach (var sqlCommand in dbCommand.InitialSetupCreateTables)
                    {
                        ExecNonQuery(connection, sqlCommand);
                        Console.WriteLine(Util.TableCreateSuccess);
                    }

                    // Insert statements.
                    foreach (var sqlCommand in dbCommand.InitialSetupInsert)
                    {
                        ExecNonQuery(connection, sqlCommand);
                        Console.WriteLine(Util.InsertStatementSuccess);
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }

        private static void ExecNonQuery(SqlConnection connection, string sqlCommand)
        {
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
