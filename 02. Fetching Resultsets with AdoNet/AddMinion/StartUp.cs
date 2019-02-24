namespace AddMinion
{
    using HelperClasses.Configurations;
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            // Input.
            string[] minionData = Console.ReadLine().Split();
            string[] vallainData= Console.ReadLine().Split();

            string minionName = minionData[1];
            int minionAge = int.Parse(minionData[2]);
            string townName = minionData[3];
            string villainName = vallainData[1];

            string[] sqlVariables;
            dynamic[] entityData;

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConfigurationString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Get town id.
                        int? townId = Service.GetEntityByName(townName, connection, "@townName", DbCommand.SelectTownId);

                        if (townId == null)
                        {
                            sqlVariables = new string[] { "@townName" };
                            entityData = new dynamic[] { townName };
                            Service.AddEntity(connection, DbCommand.InsertTown, sqlVariables, entityData);
                            transaction.Commit();
                            Print(string.Format(Util.InsertTownSuccess, townName));
                            townId = Service.GetEntityByName(townName, connection, "@townName", DbCommand.SelectTownId);
                        }

                        // Get minion.
                        sqlVariables = new string[] { "@name", "@age", "@townId" };
                        entityData = new dynamic[] { minionName, minionAge, townId };
                        transaction.Commit();
                        Service.AddEntity(connection, DbCommand.InsertMinion, sqlVariables, entityData);

                        // Get villain Id.
                        int? villainId = Service.GetEntityByName(villainName, connection, "@Name", DbCommand.SelectVillainId);

                        if (villainId == null)
                        {
                            sqlVariables = new string[] { "@villainName" };
                            entityData = new dynamic[] { villainName };
                            Service.AddEntity(connection, DbCommand.InsertVillain, sqlVariables, entityData);
                            transaction.Commit();
                            Print(string.Format(Util.InsertVillainSuccess, villainName));
                            villainId = Service.GetEntityByName(villainName, connection, "@Name", DbCommand.SelectVillainId);
                        }

                        // Add minion to villain.
                        int? minionId = Service.GetEntityByName(minionName, connection, "@Name", DbCommand.SelectMinionId);
                        sqlVariables = new string[] { "@villainId", "@minionId" };
                        entityData = new dynamic[] { villainId, minionId };
                        Service.AddEntity(connection, DbCommand.InsertMinnionVillian, sqlVariables, entityData);
                        transaction.Commit();
                        Print(string.Format(Util.InsertMinionVillainSuccess, minionName, villainName));
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();                       
                    }                                      
                }
            }
            catch (Exception e)
            {
                Print(e.Message);                          
            }            
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
