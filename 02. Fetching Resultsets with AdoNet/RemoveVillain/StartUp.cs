namespace RemoveVillain
{
    using HelperClasses;
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());
            string villainName = string.Empty;
            int releasedMinions = 0;
            string[] sqlVariables;
            dynamic[] entityData;

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
                {
                    connection.Open();
                                       
                    villainName = Service<string>.GetEntityProp(villainId, connection, "@villainId", DbCommand.SelectVillainById);

                    if (villainName == null)
                    {
                        Console.WriteLine(Util.NoViillainFound);
                        return;
                    }

                    sqlVariables = new string[] { "@villainId" };
                    entityData = new dynamic[] { villainId };

                    releasedMinions = Service<int>.ExecNonQuery(connection, DbCommand.DeleteMinionVllainById, sqlVariables, entityData);
                    Service<int>.ExecNonQuery(connection, DbCommand.DeleteVillainById, sqlVariables, entityData);
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
