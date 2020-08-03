using System.Text;
using System.Data.SqlClient;

namespace WpfFungusApp.DBStore
{
    class PostgreSQLDatabase
    {
        private static string MakeConnectionString(string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            Npgsql.NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder();
            npgsqlConnectionStringBuilder.Host = host;
            npgsqlConnectionStringBuilder.Port = port;
            npgsqlConnectionStringBuilder.IntegratedSecurity = useWindowsAuthentication;
            npgsqlConnectionStringBuilder.Username = userName;
            npgsqlConnectionStringBuilder.Password = password;
            npgsqlConnectionStringBuilder.Database = dbName;
            return npgsqlConnectionStringBuilder.ToString();
        }

        public static void CreateDatabase(IDatabaseHost databaseHost, string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            // Connect to the master DB to create the requested database

            OpenDatabase(databaseHost, host, port, useWindowsAuthentication, userName, password, "postgres");

            databaseHost.Database.Execute(@"CREATE DATABASE " + dbName + @" WITH OWNER = postgres ENCODING = 'UTF8' CONNECTION LIMIT = -1;");
            databaseHost.Database.CloseSharedConnection();

            // Connect to the new database

            OpenDatabase(databaseHost, host, port, useWindowsAuthentication, userName, password, dbName);
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            string connectionString = MakeConnectionString(host, port, useWindowsAuthentication, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "Npgsql");
            databaseHost.Database.OpenSharedConnection();
        }
    }
}

