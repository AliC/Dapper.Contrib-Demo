using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistence.Dapper
{
    public class Database
    {
        protected static string GetConnectionString(string databaseName = null)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "(LocalDB)\\MSSQLLocalDB";
            connectionStringBuilder.IntegratedSecurity = true;
            connectionStringBuilder.ConnectTimeout = 30;
            connectionStringBuilder.InitialCatalog = databaseName ?? "master";

            connectionStringBuilder.Pooling = false;

            return connectionStringBuilder.ConnectionString;
        }

        internal static IDbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString("Dapper"));
        }

        internal static void DeleteOrder(long newId)
        {
            using (IDbConnection connection = GetConnection())
            {
                using (SqlCommand command = ((SqlConnection)connection).CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "DELETE FROM Orders WHERE Id = @Id";
                    command.Parameters.AddWithValue("Id", newId);
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
