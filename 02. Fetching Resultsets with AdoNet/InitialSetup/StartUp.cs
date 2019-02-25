namespace InitialSetup
{
    using HelperClasses;
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
                using (SqlConnection connection = new SqlConnection(Configuration.ConnectionStringBeforeCreateDB))
                {
                    // Create database.
                    connection.Open();
                    Service<int>.ExecNonQuery(connection, DbCommand.InitialSetupCreateDB, null, null);
                    Console.WriteLine(Util.DBCreateSuccess);                    
                }

                using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
                {
                    connection.Open();
                    // Create tables.
                    foreach (var sqlCommand in dbCommand.InitialSetupCreateTables)
                    {
                        Service<int>.ExecNonQuery(connection, sqlCommand, null, null);
                        Console.WriteLine(Util.TableCreateSuccess);
                    }

                    // Insert statements.
                    foreach (var sqlCommand in dbCommand.InitialSetupInsert)
                    {
                        Service<int>.ExecNonQuery(connection, sqlCommand, null, null);
                        Console.WriteLine(Util.InsertStatementSuccess);
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
