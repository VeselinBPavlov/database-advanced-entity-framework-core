namespace HelperClasses
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public static class Service<T>
    {
        public static T GetEntityProp(dynamic criteria, SqlConnection connection, string sqlVariable, string sqlCommand)
        {
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                command.Parameters.AddWithValue(sqlVariable, criteria);
                return (T)command.ExecuteScalar();
            }
        }

        public static int ExecNonQuery(SqlConnection connection, string sqlCommand, string[] sqlVariables, dynamic[] entityData)
        {
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                if (sqlVariables != null)
                {
                    for (int i = 0; i < sqlVariables.Length; i++)
                    {
                        command.Parameters.AddWithValue(sqlVariables[i], entityData[i]);
                    }
                }

                return command.ExecuteNonQuery();
            }
        }       
    }
}
