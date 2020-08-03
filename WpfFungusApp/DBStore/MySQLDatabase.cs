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

            string connectionString = MakeConnectionString(host, port, useWindowsAuthentication, userName, password, "postgres");
            databaseHost.Database = new PetaPoco.Database(connectionString, "Npgsql");
            databaseHost.Database.OpenSharedConnection();
            databaseHost.Database.Execute(@"CREATE DATABASE " + dbName + @" WITH OWNER = postgres ENCODING = 'UTF8' CONNECTION LIMIT = -1;");
            databaseHost.Database.CloseSharedConnection();

            // Connect to the new database

            connectionString = MakeConnectionString(host, port, useWindowsAuthentication, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "Npgsql");
            databaseHost.Database.OpenSharedConnection();
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string host, int port, bool useWindowsAuthentication, string userName, string password, string filepath)
        {
            string dbName = System.IO.Path.GetFileNameWithoutExtension(filepath);
            string connectionString = MakeConnectionString(host, port, useWindowsAuthentication, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "Npgsql");
            databaseHost.Database.OpenSharedConnection();

            databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);
        }
    }
}
