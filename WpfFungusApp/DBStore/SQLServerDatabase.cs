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

        public static void CreateDatabase(IDatabaseHost databaseHost, string dataSource, string userName, string password, string folder, string dbName)
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
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string filepath)
        {
            // Warning warning => get the user name and password from the user ... 
            string dbName = System.IO.Path.GetFileNameWithoutExtension(filepath);
            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");
            databaseHost.Database.OpenSharedConnection();
        }
    }
}
