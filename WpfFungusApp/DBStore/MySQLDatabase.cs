using System.Text;
namespace WpfFungusApp.DBStore
{
    internal class MySQLDatabase
    {
        private static string MakeConnectionString(string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Host=");
            stringBuilder.Append(host);
            if (useWindowsAuthentication)
            {
                stringBuilder.Append("; IntegratedSecurity=true");
            }
            else
            { 
                stringBuilder.Append("; Username=");
                stringBuilder.Append(userName);
                stringBuilder.Append("; Password=");
                stringBuilder.Append(password);
            }
            stringBuilder.Append("; Database= ");
            stringBuilder.Append(dbName);
            stringBuilder.Append("; Port= ");
            stringBuilder.Append(port);

            return stringBuilder.ToString();
        }

        public static void CreateDatabase(IDatabaseHost databaseHost, string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            // Connect to the master DB to create the requested database

            OpenDatabase(databaseHost, host, port, useWindowsAuthentication, userName, password, "MySql");

            databaseHost.Database.Execute(@"CREATE DATABASE " + dbName);
            databaseHost.Database.CloseSharedConnection();

            // Connect to the new database

            OpenDatabase(databaseHost, host, port, useWindowsAuthentication, userName, password, dbName);
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string host, int port, bool useWindowsAuthentication, string userName, string password, string dbName)
        {
            string connectionString = MakeConnectionString(host, port, useWindowsAuthentication, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "MySql");
            databaseHost.Database.OpenSharedConnection();
        }
    }
}
