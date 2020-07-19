using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFungusApp.DBStore
{
    class PostgreSQLDatabase
    {
        private static string MakeConnectionString(string host, int port, string userName, string password, string dbName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Host=");
            stringBuilder.Append(host);
            if (!string.IsNullOrEmpty(userName))
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

        public static void NewDatabase(IDatabaseHost databaseHost, string host, int port, string userName, string password, string folder, string dbName)
        {
            // Connect to the master DB to create the requested database

            string connectionString = MakeConnectionString(host, port, userName, password, "postgres");
            databaseHost.Database = new PetaPoco.Database(connectionString, "Npgsql");

            databaseHost.Database.OpenSharedConnection();
            string filename = System.IO.Path.Combine(folder, dbName);
            databaseHost.Database.Execute(@"CREATE DATABASE " + dbName + @" WITH OWNER = postgres ENCODING = 'UTF8' CONNECTION LIMIT = -1;");
            databaseHost.Database.CloseSharedConnection();

            // Connect to the new database

            connectionString = MakeConnectionString(host, port, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "Npgsql");
            databaseHost.Database.OpenSharedConnection();

            databaseHost.IConfigurationStore = new DBStore.PostgreSQLConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.PostgreSQLSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.PostgreSQLImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.PostgreSQLImageStore(databaseHost.Database);

            databaseHost.IConfigurationStore.CreateTable();
            databaseHost.IConfigurationStore.Initialise();
            databaseHost.ISpeciesStore.CreateTable();
            databaseHost.IImagePathsStore.CreateTable();
            databaseHost.IImageStore.CreateTable();
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string host, int port, string userName, string password, string filepath)
        {
            string dbName = System.IO.Path.GetFileNameWithoutExtension(filepath);
            string connectionString = MakeConnectionString(host, port, userName, password, dbName);
            databaseHost.Database = new PetaPoco.Database(connectionString, "Npgsql");
            databaseHost.Database.OpenSharedConnection();
            databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);
        }
    }
}

