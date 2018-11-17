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
    }
}
