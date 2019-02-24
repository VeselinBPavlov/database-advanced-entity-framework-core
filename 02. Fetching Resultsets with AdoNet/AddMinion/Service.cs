namespace AddMinion
{
    using System.Data.SqlClient;

    public static class Service
    {
        public static int? GetEntityByName(string name, SqlConnection connection, string sqlVariable, string sqlCommand)
        {
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                command.Parameters.AddWithValue(sqlVariable, name);
                return (int?)command.ExecuteScalar();
            }
        }

        public static void AddEntity(SqlConnection connection, string sqlCommand, string[] sqlVariables, dynamic[] entityData)
        {
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                for (int i = 0; i < sqlVariables.Length; i++)
                {
                    command.Parameters.AddWithValue(sqlVariables[i], entityData[i]);
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
