using System.Text;
using System.Data.SqlClient;

namespace WpfFungusApp.DBStore
{
    internal class SQLServerDatabase
    {
        private static string MakeConnectionString(bool useDataSource, string dataSource, string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            if (useDataSource)
            {
                sqlConnectionStringBuilder.DataSource = dataSource;
            }
            else
            {
                sqlConnectionStringBuilder.DataSource = host + "," + port;
            }

            if (useWindowsAuthentication)
            {
                sqlConnectionStringBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlConnectionStringBuilder.UserID = userName;
                sqlConnectionStringBuilder.Password = password;
            }

            sqlConnectionStringBuilder.InitialCatalog = dbName;
            return sqlConnectionStringBuilder.ToString();
        }

        public static void CreateDatabase(IDatabaseHost databaseHost, string dataSource, bool useWindowsAuthentication, string userName, string password, string folder, string dbName)
        {
            // Connect to the master DB to create the requested database

            OpenDatabase(databaseHost, true, dataSource, null, -1, useWindowsAuthentication, userName, password, "master");

            string filename = System.IO.Path.Combine(folder, dbName);
            databaseHost.Database.Execute("CREATE DATABASE " + dbName + " ON PRIMARY (Name=" + dbName + ", filename = \"" + filename + ".mdf\") LOG ON (name=" + dbName + "_log, filename=\"" + filename + ".ldf\")");
            databaseHost.Database.CloseSharedConnection();

            // Connect to the new database

            OpenDatabase(databaseHost, true, dataSource, null, -1, useWindowsAuthentication, userName, password, dbName);
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, bool useDataSource, string dataSource, string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            string connectionString = MakeConnectionString(useDataSource, dataSource, host, port, useWindowsAuthentication, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "System.Data.SqlClient");
            databaseHost.Database.OpenSharedConnection();
        }
    }
}
