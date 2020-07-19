using System.Text;

namespace WpfFungusApp.DBStore
{
    internal class SQLServerDatabase
    {
        private static string MakeConnectionString(string dataSource, string userName, string password, string dbName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Data Source=");
            stringBuilder.Append(dataSource);
            if (string.IsNullOrEmpty(userName))
            {
                stringBuilder.Append("; Integrated security = SSPI; database = ");
            }
            else
            {
                stringBuilder.Append("; User Id=");
                stringBuilder.Append(userName);
                stringBuilder.Append("; Password=");
                stringBuilder.Append(password);
                stringBuilder.Append("; database = ");
            }

            stringBuilder.Append(dbName);
            return stringBuilder.ToString();
        }

        public static void NewDatabase(IDatabaseHost databaseHost, string dataSource, string userName, string password, string folder, string dbName)
        {
            // Connect to the master DB to create the requested database

            string connectionString = MakeConnectionString(dataSource, userName, password, "master");
            databaseHost.Database = new PetaPoco.Database(connectionString, "System.Data.SqlClient");
        
            databaseHost.Database.OpenSharedConnection();
            string filename = System.IO.Path.Combine(folder, dbName);
            databaseHost.Database.Execute("CREATE DATABASE " + dbName + " ON PRIMARY (Name=" + dbName + ", filename = \"" + filename + ".mdf\") LOG ON (name=" + dbName + "_log, filename=\"" + filename + ".ldf\")");
            databaseHost.Database.CloseSharedConnection();

            // Connect to the new database

            connectionString = MakeConnectionString(dataSource, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "System.Data.SqlClient");
            databaseHost.Database.OpenSharedConnection();

            databaseHost.IConfigurationStore = new DBStore.SQLServerConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLServerSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLServerImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLServerImageStore(databaseHost.Database);

            databaseHost.IConfigurationStore.CreateTable();
            databaseHost.IConfigurationStore.Initialise();
            databaseHost.ISpeciesStore.CreateTable();
            databaseHost.IImagePathsStore.CreateTable();
            databaseHost.IImageStore.CreateTable();
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string filepath)
        {
            string dbName = System.IO.Path.GetFileNameWithoutExtension(filepath);
            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");

            databaseHost.Database.OpenSharedConnection();
            databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);
        }
    }
}
